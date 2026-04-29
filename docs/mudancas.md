# Mudanças

Projeto original feito em TS com NEST e NEXTJS.
Essa é uma adaptação para fins de estudo para .NET10 e Angular 21.

### Alterações gerais

- Criado solução para conter os projeto ao invés do turborepo.
- Seguido a nomenclatura do padrão .NET para classes e métodos.
  - Utilizado `PascalCase` para nomes de classes.
  - Interfaces seguindo o padrão `I` + nome da interface.
  - Métodos assíncronos terminando com `Async`.
- Para Id e outros objetos que necessitam de um identificador único foi utilizado o Guid nativo do .NET com novo método `Guid.CreateVersion7()` mais moderno e performático. Substituindo o ValueObject Id.
- Usado tipo `record` para value objects devido a sua imutabilidade e comparação por valor, dispensando classe base.
- Alterado pasta `packages` para `pkgs` pois nome usado historicamente para cache/restauração de dependências NuGet e evita manipulações no `gitignore`.
- Utilizado `xunit` para testes unitários.
