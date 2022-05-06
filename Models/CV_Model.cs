using System.ComponentModel.DataAnnotations.Schema;

namespace CV_Maker_with_RDLC_Report_.Net_Core_MVC.Models
{
    public class CV_Model
    {
        public int Id { get; set; }
        public string? name { get; set; }
        public string? address_contact { get; set; }
        public string? careerObjective { get; set; }
        public string? specialQualification { get; set; }
        public string? projectsHistory { get; set; }
        public string? titleOne { get; set; }
        public string? titleOneDecs { get; set; }
        public string? awardsCertificate { get; set; }
        public string? profile { get; set; }

        //Tranning Info
        public string? trainingTitle { get; set; }
        public string? topic { get; set; }
        public string? institute { get; set; }
        public string? country { get; set; }
        public string? location { get; set; }
        public string? year { get; set; }

        //University Info
        public string? degree_title { get; set; }
        public string? major { get; set; }
        public string? varsity { get; set; }
        public string? result { get; set; }
        public string? passYear { get; set; }
        public string? duration { get; set; }

        //Collage Info
        public string? titleHS { get; set; }
        public string? majorHS { get; set; }
        public string? collage { get; set; }
        public string? resultHS { get; set; }
        public string? passYearHS { get; set; }
        public string? durationHS { get; set; }

        [NotMapped]
        public IFormFile? imgFile { get; set; }



    }

}
