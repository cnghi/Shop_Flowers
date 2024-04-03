using System.ComponentModel.DataAnnotations;

namespace Shop_Flowers.Reponsitory.Validation
{
    public class FileExtensionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);
                string[] extensions = { "jpg", "png", "jpeg" };
                bool result = extensions.Any(x => x.EndsWith(x));

                if(!result)
                {
                    return new ValidationResult("Những định dạng hình ảnh được cho phép là jpg, png, jpeg");
                }
            }
            return ValidationResult.Success;
        }
    }
}
