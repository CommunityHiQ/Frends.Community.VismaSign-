using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#pragma warning disable 1591

namespace Frends.Community.VismaSign
{
    public class DocumentCreateInput
    {
        /// <summary>
        /// Body in JSON format
        /// </summary>
        [DisplayFormat(DataFormatString = "Json")]
        public dynamic Body { get; set; }
    }

    public class DocumentAddFileInput
    {
        /// <summary>
        /// Uri id of the document
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        public string DocumentUriId { get; set; }

        /// <summary>
        /// Read data from a pdf file
        /// </summary>
        public bool ReadFromFile { get; set; }

        /// <summary>
        /// Byte Array of the pdf
        /// </summary>
        [DisplayFormat(DataFormatString = "Expression")]
        [UIHint(nameof(ReadFromFile), "", false)]
        public byte[] InputBytes { get; set; }

        /// <summary>
        /// Location of the pdf file
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        [UIHint(nameof(ReadFromFile), "", true)]
        public string FileLocation { get; set; }

        /// <summary>
        /// Optional setting to add file name
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        public string FileName { get; set; }
    }

    public class DocumentAddInvitationsInput
    {
        /// <summary>
        ///  Uri id of the document 
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        public string DocumentUriId { get; set; }

        /// <summary>
        /// Body as json
        /// </summary>
        [DisplayFormat(DataFormatString = "Json")]
        public string Body { get; set; }
    }

    public class DocumentSearchInput
    {
        /// <summary>
        /// Query parameters
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        public dynamic Query { get; set; }
    }

    public class DocumentGetInput
    {
        /// <summary>
        ///  Uri id of the document 
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        public dynamic DocumentUriId { get; set; }
    }

    public class ConnectionOption
    {
        [DisplayFormat(DataFormatString = "Expression")]
        public string Identifier { get; set; }
		[PasswordPropertyText]
        [DisplayFormat(DataFormatString = "Expression")]
        public string Secret { get; set; }
        [DisplayFormat(DataFormatString = "Expression")]
        public string BaseAddress { get; set; }
        /// <summary>
        /// Throw exception if return code of request is not successful
        /// </summary>
        public bool ThrowExceptionOnErrorResponse { get; set; }
    }

    public class HttpResponse
    {
        public string Location { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public int StatusCode { get; set; }
    }
    public class HttpResponseWithBody
    {
        public string Body { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public int StatusCode { get; set; }
    }
}
