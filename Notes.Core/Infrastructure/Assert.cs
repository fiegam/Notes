using System;

namespace Notes.Core.Infrastructure
{
    public static class Assert
    {
        public static void NotNull(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}