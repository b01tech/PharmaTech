using PharmaTech.Shared.Core;

namespace PharmaTech.Shared.Tests.Core;

public class ResultTests
{
    [Fact]
    public void Success_Should_Return_Without_Error()
    {
        var result = Result.Success();

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Empty(result.Errors);
    }
    [Fact]
    public void Success_Should_Return_Data()
    {
        var result = Result<string>.Success("Test");

        Assert.Equal("Test", result.Data);
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Empty(result.Errors);
    }
    [Fact]
    public void Failure_Should_Return_Error()
    {
        var errors = new List<string> { "ERROR_1", "ERROR_2" };
        var result = Result<string>.Failure(errors);

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(errors, result.Errors);
        Assert.Null(result.Data);
    }
    [Fact]
    public void Failure_WithSingleError_Should_Return_Error()
    {
        var result = Result.Failure("ERROR");

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Single(result.Errors);
        Assert.Equal("ERROR", result.Errors[0]);
    }
    [Fact]
    public void ImplicitConversion_Should_Return_Success_WithData()
    {
        Result<string> result = "Test";

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal("Test", result.Data);
        Assert.Empty(result.Errors);
    }
    [Fact]
    public void MergeErrors_Should_Return_All_Errors()
    {
        var result1 = Result<string>.Failure("ERROR_1");
        var result2 = Result<string>.Failure("ERROR_2");
        var result3 = Result<string>.Failure("ERROR_3");
        var errors = Result.MergeErrors(result1, result2, result3);

        Assert.Equal(new List<string> { "ERROR_1", "ERROR_2", "ERROR_3" }, errors);
    }
}
