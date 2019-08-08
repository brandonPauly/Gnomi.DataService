using gnomi.common.general;
using gnomi.dataService.entities;
using System;
using System.Collections.Generic;

namespace gnomi.repositories.utility
{
    public class fieldSkipHelper : iFieldSkipHelper
    {
        private HashSet<pair<Type, string>> _skipMap;

        public fieldSkipHelper()
        {
            _skipMap = generateSkipMap();
        }

        private HashSet<pair<Type, string>> generateSkipMap()
        {
            return new HashSet<pair<Type, string>>
            {
                new pair<Type,string>(typeof(human<long>), "humanId")
            };
        }

        public bool shouldSkip(Type type, string fieldName)
        {
            var pair = new pair<Type, string>(type, fieldName);
            return _skipMap.Contains(pair);
        }
    }
}
