using System.Collections.Generic;
using System.Linq;

namespace Common.Results
{
    public class Result<T>
    {
        public T Value { get; }
        public bool Success { get; }
        public List<string> Errors { get; }

        private Result(T value, bool success, List<string> errors)
        {
            Value = value;
            Success = success;
            Errors = errors ?? new List<string>();
        }

        public static Result<T> Ok(T value) => new Result<T>(value, true, null);
        public static Result<T> Fail(params string[] errors) => new Result<T>(default, false, errors.ToList());
    }
}
