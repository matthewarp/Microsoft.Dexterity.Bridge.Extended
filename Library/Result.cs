using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended
{
    public class Result
    {
        public static readonly Result SUCCESS = new Result(string.Empty, true);
        public static readonly Result FAIL = new Result(string.Empty, false);

        public string Message { get; }

        public bool Success { get; }

        public Result(string message = default, bool success = false)
        {
            Message = message ?? string.Empty;
            Success = success;
        }

        public static implicit operator bool(Result obj) => obj.Success;

        public static implicit operator Result(bool success) => new Result(success: success);
    }
}
