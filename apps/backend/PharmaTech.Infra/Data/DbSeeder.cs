using ProductModel = PharmaTech.Product.Product.Models.Product;
using System.Text;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using PharmaTech.Product.Category.Models;

namespace PharmaTech.Infra.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(PharmaTechDbContext context)
    {
        if (await context.Categories.AnyAsync())
            return;

        var categories = new List<Category>();
        var products = new List<ProductModel>();

        // 1. Create Categories and Subcategories
        var categoryData = new (string Name, string[] Subcategories)[]
        {
            ("Medicamentos", ["Analgésicos", "Antibióticos", "Anti-inflamatórios", "Antialérgicos"]),
            ("Vitaminas e Suplementos", ["Multivitamínicos", "Minerais", "Suplementos Esportivos"]),
            ("Cuidados Pessoais", ["Higiene Bucal", "Cuidados com a Pele", "Cabelo"]),
            ("Mamãe e Bebê", ["Fraldas", "Alimentos Infantis", "Acessórios"]),
            ("Dermocosméticos", ["Proteção Solar", "Anti-idade", "Hidratantes"]),
            ("Primeiros Socorros", ["Curativos", "Antissépticos", "Bandagens"]),
            ("Ortopedia", ["Joelheiras", "Tornozeleiras", "Munhequeiras"]),
            ("Saúde Sexual", ["Preservativos", "Lubrificantes"]),
            ("Gripe e Resfriado", ["Xaropes", "Descongestionantes", "Antitérmicos"]),
            ("Genéricos", ["Referência", "Similar"])
        };

        foreach (var (catName, subNames) in categoryData)
        {
            var categoryResult = Category.Create(catName, Slugify(catName));
            if (categoryResult.IsFailure) continue;

            var category = categoryResult.Data;

            foreach (var subName in subNames)
            {
                var subResult = Subcategory.Create(subName, Slugify(subName), category.Id);
                if (subResult.IsSuccess)
                {
                    category.Add(subResult.Data);
                }
            }
            categories.Add(category);
        }

        await context.Categories.AddRangeAsync(categories);
        await context.SaveChangesAsync();

        // 2. Create Products (30 items)
        var allSubcategories = categories.SelectMany(c => c.Subcategories).ToList();

        var productData = new (string Name, string Desc, decimal Price, string Sku)[]
        {
             ("Paracetamol 750mg", "Analgésico e antitérmico para alívio da dor e febre.", 15.50m, "PARA-750"),
             ("Ibuprofeno 400mg", "Anti-inflamatório para dores musculares e febre.", 22.90m, "IBU-400"),
             ("Amoxicilina 500mg", "Antibiótico de amplo espectro para infecções bacterianas.", 35.00m, "AMOX-500"),
             ("Vitamina C 1g", "Suplemento vitamínico para imunidade.", 12.00m, "VITC-1000"),
             ("Dipirona Monohidratada", "Analgésico potente para dores de cabeça e corpo.", 8.50m, "DIP-500"),
             ("Loratadina 10mg", "Antialérgico para rinite e urticária.", 18.00m, "LORA-10"),
             ("Ômega 3 1000mg", "Suplemento de óleo de peixe para saúde cardiovascular.", 45.00m, "OMEGA-3"),
             ("Shampoo Anticaspa", "Limpeza profunda e controle da caspa.", 25.90m, "SHAMP-CASPA"),
             ("Fralda Descartável M", "Pacote com 40 unidades, absorção prolongada.", 55.00m, "FRALDA-M"),
             ("Protetor Solar FPS 50", "Alta proteção contra raios UVA e UVB.", 65.00m, "SOLAR-50"),
             ("Creme Hidratante Facial", "Hidratação intensa para pele seca.", 42.00m, "HIDRAT-FACE"),
             ("Curativo Adesivo", "Caixa com 10 unidades, resistente à água.", 5.00m, "CURATIVO-10"),
             ("Joelheira Elástica", "Suporte e compressão para o joelho.", 35.00m, "JOELHEIRA-M"),
             ("Preservativo Lubrificado", "Pacote com 3 unidades, segurança e conforto.", 7.50m, "PRESERV-3"),
             ("Xarope Expectorante", "Alívio da tosse com catarro.", 28.00m, "XAROPE-EXP"),
             ("Descongestionante Nasal", "Alívio rápido da congestão nasal.", 14.00m, "DESCONG-NASAL"),
             ("Simeticona 125mg", "Alívio de gases e desconforto abdominal.", 10.00m, "SIMET-125"),
             ("Dorflex", "Relaxante muscular e analgésico.", 12.50m, "DORFLEX-10"),
             ("Neosaldina", "Para dores de cabeça e enxaqueca.", 18.90m, "NEOSA-10"),
             ("Buscopan Composto", "Alívio rápido de cólicas e dores abdominais.", 20.00m, "BUSCO-CP"),
             ("Cenevit Zinco", "Vitamina C + Zinco para imunidade.", 15.00m, "CENEVIT-ZN"),
             ("Centrum", "Multivitamínico completo de A a Z.", 80.00m, "CENTRUM-60"),
             ("Bepantol Derma", "Hidratante labial regenerador.", 25.00m, "BEPAN-LAB"),
             ("Sabonete Líquido Facial", "Limpeza suave para pele oleosa.", 32.00m, "SAB-FACIAL"),
             ("Lenços Umedecidos", "Limpeza delicada para bebês.", 12.00m, "LENCO-BEBE"),
             ("Pomada para Assaduras", "Proteção contra assaduras em bebês.", 18.00m, "POMADA-ASS"),
             ("Álcool 70%", "Antisséptico para mãos e superfícies.", 8.00m, "ALCOOL-70"),
             ("Termômetro Digital", "Medição precisa da temperatura corporal.", 25.00m, "TERM-DIG"),
             ("Vick Vaporub", "Alívio da tosse e congestão nasal.", 22.00m, "VICK-VAP"),
             ("Eno", "Sal de frutas para azia e má digestão.", 4.00m, "ENO-SACHE")
        };

        var random = new Random();
        foreach (var p in productData)
        {
            if (allSubcategories.Count == 0) break;

            var subcategory = allSubcategories[random.Next(allSubcategories.Count)];

            var productResult = ProductModel.Create(
                p.Name,
                Slugify(p.Name),
                p.Desc,
                p.Sku,
                p.Price,
                subcategory.Id
            );

            if (productResult.IsSuccess)
            {
                products.Add(productResult.Data);
            }
        }

        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();
    }

    private static string Slugify(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return "";

        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC)
            .ToLower()
            .Replace(" ", "-")
            .Replace("%", "")
            .Replace(".", "")
            .Replace(",", "");
    }
}
