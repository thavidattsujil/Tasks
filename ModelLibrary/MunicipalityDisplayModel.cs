using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelLibrary
{
    public class MunicipalityDisplayModel
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public DateTime ReleaseDate { get; set; }
        public string Result { get; set; }
    }


    public class MunicipalityInputModel
    {

        public string Name { get; set; }

        public string Date { get; set; }

        public string TaxType { get; set; }
    }

    public class RootObject
    {
        public List<MunicipalityInputModel> info { get; set; }
    }
}
