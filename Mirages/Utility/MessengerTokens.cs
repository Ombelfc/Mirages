using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirages.Utility
{
    public static class MessengerTokens
    {
        public static Guid MouseWheenSpin { get; } = Guid.NewGuid();
        public static Guid KeyPressed { get; } = Guid.NewGuid();
    }
}
