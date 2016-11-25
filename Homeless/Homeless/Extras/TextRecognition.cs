using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using System.IO;
using System.Diagnostics;
namespace Homeless.Extras
{
    public class TextRecognition
    {

        public async Task<string> GetText(Stream image)
        {
            try
            {
                OcrResults text;
                var client = new VisionServiceClient("c14ab52921fc4f6d82a778c9bcc90dbd");


                image.Seek(0, SeekOrigin.Begin);
                text = await client.RecognizeTextAsync(image);
                return ShowRetrieveText(text);
            }
            catch(Exception ex)
            {
                return ex.Message;   
            }
        }

        private string ShowRetrieveText(OcrResults results)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (results != null && results.Regions != null)
            {
                stringBuilder.Append("Text: ");
                stringBuilder.AppendLine();
                foreach (var item in results.Regions)
                {
                    foreach (var line in item.Lines)
                    {
                        foreach (var word in line.Words)
                        {
                            stringBuilder.Append(word.Text);
                            stringBuilder.Append(" ");
                        }

                        stringBuilder.AppendLine();
                    }

                    stringBuilder.AppendLine();
                }
            }

            return stringBuilder.ToString();
        }
    }
}
