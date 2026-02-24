using PharmaTech.Core.Base;

namespace PharmaTech.Core.Test.Base;

public class ResultTests
{
    [Fact]
    public void Success_should_create_success_result_without_errors()
    {
        var result = Result.Success();

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Failure_with_list_should_create_failure_result_with_errors()
    {
        var errors = new List<string> { "ERROR_1", "ERROR_2" };

        var result = Result.Failure(errors);

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(errors, result.Errors);
        Assert.NotSame(errors, result.Errors);
    }

    [Fact]
    public void Failure_with_single_error_should_create_failure_result_with_one_error()
    {
        var result = Result.Failure("ERROR");

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Single(result.Errors);
        Assert.Equal("ERROR", result.Errors[0]);
    }

    [Fact]
    public void Generic_success_should_create_success_result_with_data()
    {
        var data = 42;

        var result = Result<int>.Success(data);

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Empty(result.Errors);
        Assert.Equal(data, result.Data);
    }

    [Fact]
    public void Generic_failure_with_list_should_create_failure_result_with_errors_and_default_data()
    {
        var errors = new List<string> { "ERROR_1", "ERROR_2" };

        var result = Result<int>.Failure(errors);

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(errors, result.Errors);
        Assert.Equal(default, result.Data);
    }

    [Fact]
    public void Generic_failure_with_single_error_should_create_failure_result_with_default_data()
    {
        var result = Result<int>.Failure("ERROR");

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Single(result.Errors);
        Assert.Equal("ERROR", result.Errors[0]);
        Assert.Equal(default, result.Data);
    }

    [Fact]
    public void Implicit_conversion_should_create_success_result_from_data()
    {
        Result<string> result = "value";

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Empty(result.Errors);
        Assert.Equal("value", result.Data);
    }
}
