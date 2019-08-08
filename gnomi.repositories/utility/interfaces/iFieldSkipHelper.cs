using System;

namespace gnomi.repositories.utility
{
    public interface iFieldSkipHelper
    {
        bool shouldSkip(Type type, string fieldName);
    }
}
