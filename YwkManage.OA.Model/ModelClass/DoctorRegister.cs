using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  YwkManage.OA.Model.ModelClass
{
    public partial class DoctorRegister
    {
        public int DoctorRegisterID { get; set; }
        public string EmployeeID { get; set; }
        public string CertifiedStatus { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string IDCard { get; set; }
        public string QualificationCertificateNo { get; set; }
        public string LicensedCertificateNo { get; set; }
        public string DoctorGrade { get; set; }
        public string LicensedType { get; set; }
        public string DoctorTitle { get; set; }
        public string LicensedScope { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string ApprovalOrganization { get; set; }
        public string MedicalInstitutions { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string RegisterStatus { get; set; }
    }
}
