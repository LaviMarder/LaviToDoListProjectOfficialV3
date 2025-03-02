using Android.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LaviToDoListProjectOfficialV3.Helpers
{
    public class General
    {

        public const int REQUEST_OPEN_CAMERA = 1;

        public const string KEY_CAMERA_IMAGE = "data";



        public const string KEY_ISADMIN = "IsAdmin";

        public const string FS_COLLECTION = "UsersCollection";

        public const string FS_TASK_COLLECTION = "TaskCollection";


        public const string FS_IMAGES = "Images/";



        public const string KEY_ID = "Id";

        public const string KEY_USERNAME = "Username";

        public const string KEY_FULLNAME = "FullName";

        public const string KEY_MAIL = "Mail";

        public const string KEY_PASS = "Password";



        public const string KEY_URL = "ImageURL";
        public static byte[] BitmapToByteArray(Bitmap bitmap)
        {
            MemoryStream ms = new MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Png, 100, ms);
            return ms.ToArray();
        }

        public static Bitmap ByteArrayToBitmap(byte[] bytes)
        {
            return BitmapFactory.DecodeByteArray(bytes, 0, bytes.Length);
        }

    }
}


