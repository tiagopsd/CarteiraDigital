using CarteiraDigital.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarteiraDigital.Test
{
    public class ContextFake
    {
        public Context CreateContext()
        {
            var op = new DbContextOptionsBuilder<Context>();
            op.UseInMemoryDatabase("CarteiraDigital");

            var fakeContext = new Context(op.Options);
            return fakeContext;
        }
    }
}
