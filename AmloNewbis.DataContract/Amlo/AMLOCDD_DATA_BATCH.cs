using System;
using System.Collections.Generic;
using System.Text;

namespace AmloNewbis.DataContract
{
    public class AMLOCDD_DATA_BATCH
    {
        public string ADDRESS_COUNTRY { get; set; }

        public string BIRTHINFO_DATE { get; set; }

        public DateTime? BIRTH_DATE { get; set; }

        public string BIRTH_PLACE { get; set; }

        public int DATA_ID { get; set; }

        public string ENTITY_ID { get; set; }

        public string ENTITY_TYPE { get; set; }
        public string GENDER { get; set; }

        public string ID_TYPE { get; set; }
        public string ID_VALUE { get; set; }

        public int IMPORT_ID { get; set; }
        public string INFO_SOURCE { get; set; }

        public string ORIGINAL_SCRIPT_NAME { get; set; }
        public string SINGLE_STRING_NAME { get; set; }
    }
}
