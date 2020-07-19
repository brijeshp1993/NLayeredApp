using System.IO;
using System.Net.Mime;
using System.Net.Mail;

namespace Demo.EmailServices
{
    public class MailAttachment
    {
        #region Fields

        private readonly MemoryStream _stream;
        private readonly string _filename;
        private readonly string _mediaType;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the data stream for this attachment
        /// </summary>
        public Stream Data
        {
            get { return _stream; }
        }

        /// <summary>
        /// Gets the original filename for this attachment
        /// </summary>
        public string Filename
        {
            get { return _filename; }
        }

        /// <summary>
        /// Gets the attachment type: Bytes or String
        /// </summary>
        public string MediaType
        {
            get { return _mediaType; }
        }

        /// <summary>
        /// Gets the file for this attachment (as a new attachment)
        /// </summary>
        public Attachment File
        {
            get { return new Attachment(Data, Filename, MediaType); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Construct a mail attachment form a byte array
        /// </summary>
        /// <param name="data">Bytes to attach as a file</param>
        /// <param name="filename">Logical filename for attachment</param>
        public MailAttachment(byte[] data, string filename)
        {
            this._stream = new MemoryStream(data);
            this._filename = filename;
            this._mediaType = MediaTypeNames.Application.Octet;
        }

        /// <summary>
        /// Construct a mail attachment from a string
        /// </summary>
        /// <param name="data">String to attach as a file</param>
        /// <param name="filename">Logical filename for attachment</param>
        public MailAttachment(string data, string filename)
        {
            this._stream = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(data));
            this._filename = filename;
            this._mediaType = MediaTypeNames.Text.Html;
        }

        #endregion
    }
}