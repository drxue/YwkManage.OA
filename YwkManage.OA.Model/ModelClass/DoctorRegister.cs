using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  YwkManage.OA.Model
{
    public partial class DoctorRegister
    {
        [Key]
        public int DID { get; set; }

        [StringLength(10)]
        public string EmployeeID { get; set; }

        [Required]
        [StringLength(10)]
        public string CertifiedStatus { get; set; }

        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        [StringLength(1)]
        public string Gender { get; set; }

        public DateTime Birthday { get; set; }

        [Required]
        [StringLength(17)]
        public string IDCard { get; set; }

        [Required]
        [StringLength(27)]
        public string QualificationCertificateNo { get; set; }

        [Required]
        [StringLength(15)]
        public string LicensedCertificateNo { get; set; }

        [Required]
        [StringLength(6)]
        public string DoctorGrade { get; set; }

        [Required]
        [StringLength(10)]
        public string LicensedType { get; set; }

        [StringLength(50)]
        public string DoctorTitle { get; set; }

        [Required]
        [StringLength(10)]
        public string LicensedScope { get; set; }

        public DateTime? ApprovalDate { get; set; }

        [StringLength(50)]
        public string ApprovalOrganization { get; set; }

        [StringLength(50)]
        public string MedicalInstitutions { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string RegisterStatus { get; set; }
    }
}
