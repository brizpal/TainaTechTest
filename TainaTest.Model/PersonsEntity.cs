using System;
using System.ComponentModel.DataAnnotations;

namespace TainaTest.Model
{
    public class PersonsEntity : IDisposable
    {
        #region Class Public Methods 

        /// <summary> 
        /// Purpose: Implements the IDispose interface. 
        /// </summary> 
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Class Property Declarations 

        [Required(ErrorMessage = "You must enter a Person ID.")]
        public int PersonId { get; set; }

        [Required(ErrorMessage = "You must enter a Person's First Name.")]
        [StringLength(30, MinimumLength = 1)]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "You must enter a Person's  Surname.")]
        [StringLength(40, MinimumLength = 1)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "You must enter a Person's  Gender.")]
        [StringLength(6, MinimumLength = 1)]
        public string Gender { get; set; }

        [Required(ErrorMessage = "You must enter a Person's  Email Address.")]
        [StringLength(200, MinimumLength = 1)]
        public string EmailAddress { get; set; }

      
        [StringLength(30, MinimumLength = 10)]
        public string PhoneNumber { get; set; }



        #endregion
    }
}
