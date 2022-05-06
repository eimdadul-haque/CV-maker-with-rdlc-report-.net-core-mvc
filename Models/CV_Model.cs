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
        public List<TrainingSummaryModel>? trainingSummary { get; set; }
        public List<AcademicQualificationModel>? academicQualificatio { get; set; }
        public string? awardsCertificate { get; set; }
        public string? profile { get; set; }

        [NotMapped]
        public IFormFile? imgFile { get; set; }



    }

}
