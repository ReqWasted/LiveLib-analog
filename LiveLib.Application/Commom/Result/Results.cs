using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Commom.Result
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string? Error { get; }

        protected Result(bool isSuccess, string? error)
        {
            if (isSuccess && error != null)
                throw new InvalidOperationException("Success result cannot have an error message.");
            if (!isSuccess && error == null)
                throw new InvalidOperationException("Failure result must have an error message.");

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new Result(true, null);
        public static Result Failure(string error) => new Result(false, error);
        public static Result<T> Success<T>(T value) => Result<T>.Success(value);
    }

    public class Result<T> : Result
    {
        public T? Value { get; }

        protected internal Result(T? value, bool isSuccess, string? error) : base(isSuccess, error)
        {
            Value = value;
        }

        public static Result<T> Success(T value) => new Result<T>(value, true, null);

        public static new Result<T> Failure(string error) => new Result<T>(default, false, error);
    }
}
