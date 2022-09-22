
using Lazy.Captcha.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.xUnit
{
    public class EvaluationEngineTest
    {
        private EvaluationEngine engine = new EvaluationEngine();

        [Fact]
        public void Test1()
        {
            var result = engine.Evaluate("1 + 1");
            Assert.Equal(2, result);
        }

        [Fact]
        public void Test2()
        {
            var result = engine.Evaluate("3 + 8");
            Assert.Equal(11, result);
        }

        [Fact]
        public void Test3()
        {
            var result = engine.Evaluate("11 + 5");
            Assert.Equal(16, result);
        }

        [Fact]
        public void Test4()
        {
            var result = engine.Evaluate("11 - 5");
            Assert.Equal(6, result);
        }

        [Fact]
        public void Test5()
        {
            var result = engine.Evaluate("7 - 9");
            Assert.Equal(-2, result);
        }

        [Fact]
        public void Test6()
        {
            var result = engine.Evaluate("0-0");
            Assert.Equal(0, result);
        }

        [Fact]
        public void Test7()
        {
            var result = engine.Evaluate("5*0");
            Assert.Equal(0, result);
        }

        [Fact]
        public void Test8()
        {
            var result = engine.Evaluate("5*3");
            Assert.Equal(15, result);
        }

        [Fact]
        public void Test9()
        {
            var result = engine.Evaluate("5*10");
            Assert.Equal(50, result);
        }

        [Fact]
        public void Test10()
        {
            var result = engine.Evaluate("8x2");
            Assert.Equal(16, result);
        }
    }
}
