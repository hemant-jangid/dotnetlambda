using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DotnetLambda
{
    public class Function
    {
        
        /// <summary>
        /// A simple function that takes a string and returns both the upper and lower case version of the string.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Casing FunctionHandler(Input input, ILambdaContext context)
        {
            return new Casing(input?.Name1.ToLower(), input?.Name2.ToUpper());
        }
    }

    public record Input(string Name1, string Name2);
    public record Casing(string Lower, string Upper);
}
