// See https://aka.ms/new-console-template for more information
using Lazy.Captcha.Core;
using Lazy.Captcha.Core.Generator;
using Lazy.Captcha.Core.Generator.Code;
using Lazy.Captcha.Test;
using SixLabors.ImageSharp;
using System.Diagnostics;

Console.WriteLine("Hello, World!");


var evaluator = new ExpressionEvaluator();
var r = evaluator.Evaluate("2*2");

Console.WriteLine(r.GetType());
