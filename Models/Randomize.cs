using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Text.RegularExpressions;

namespace Danger.Models
{
    public class Randomize
    {
        private string comments,usertag;
        public int Id { get; set; }
        public string Comments
        {
            get
            {
                return comments;
            }
            set
            {
                comments = value;
                if (comments.Contains("@"))
                {
                    string[] Split = comments.Split(' ');
                    foreach (var c in Split)
                    {
                        if (c.StartsWith("@"))
                        {
                            usertag = c;
                            comments = comments.Replace(usertag, "");
                            comments = Regex.Replace(comments, @"\s+", " ");
                        }
                    }
                }
                else
                {
                    usertag = "None";
                }
            }
        }
        public string UserTag
        {
            get
            {
                return usertag;
            }
            set
            {
                usertag = value;
            }
        }

        public Randomize()
        {

        }
    }
}
