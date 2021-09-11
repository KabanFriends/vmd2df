using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using System.Numerics;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.IO.Compression;

namespace vmd2df
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string filename = "";
            while (true)
            {
                Console.Write("enter vmd file name:");
                filename = Console.ReadLine();

                if (File.Exists(filename)) break;
                else Console.WriteLine("not found");
            }

            VMDFormat vmd = VMDReader.Import(filename);

            var dict = vmd.motion_list.motion;
            List<string> names = new List<string>();

            foreach (KeyValuePair<string, List<VMDFormat.Motion>> entry in dict)
            {
                names.Add(entry.Key);
            }

            Console.WriteLine("available bone names (" + names.Count + "): "  + string.Join(", ", names.ToArray()));

            string headName = "";
            string bodyName = "";
            string leftLegName = "";
            string rightLegName = "";
            string leftArmName = "";
            string rightArmName = "";
            string rotationName = "";

            Console.WriteLine("");
            while (true)
            {
                Console.Write("bone to apply for head:");
                headName = Console.ReadLine();

                if (names.Contains(headName)) break;
                else Console.WriteLine("not found");
            }

            while (true)
            {
                Console.Write("bone to apply for body:");
                bodyName = Console.ReadLine();

                if (names.Contains(bodyName)) break;
                else Console.WriteLine("not found");
            }

            while (true)
            {
                Console.Write("bone to apply for leftLeg:");
                leftLegName = Console.ReadLine();

                if (names.Contains(leftLegName)) break;
                else Console.WriteLine("not found");
            }

            while (true)
            {
                Console.Write("bone to apply for rightLeg:");
                rightLegName = Console.ReadLine();

                if (names.Contains(rightLegName)) break;
                else Console.WriteLine("not found");
            }

            while (true)
            {
                Console.Write("bone to apply for leftArm:");
                leftArmName = Console.ReadLine();

                if (names.Contains(leftArmName)) break;
                else Console.WriteLine("not found");
            }

            while (true)
            {
                Console.Write("bone to apply for rightArm:");
                rightArmName = Console.ReadLine();

                if (names.Contains(rightArmName)) break;
                else Console.WriteLine("not found");
            }

            while (true)
            {
                Console.Write("bone to apply for rotation:");
                rotationName = Console.ReadLine();

                if (names.Contains(rotationName)) break;
                else Console.WriteLine("not found");
            }

            Console.WriteLine("");

            List<string> headMoveList = new List<string>();
            List<string> bodyMoveList = new List<string>();
            List<string> leftLegMoveList = new List<string>();
            List<string> rightLegMoveList = new List<string>();
            List<string> leftArmMoveList = new List<string>();
            List<string> rightArmMoveList = new List<string>();
            List<string> rotationMoveList = new List<string>();

            StringBuilder sbHead = new StringBuilder();
            StringBuilder sbBody = new StringBuilder();
            StringBuilder sbLeftLeg = new StringBuilder();
            StringBuilder sbRightLeg = new StringBuilder();
            StringBuilder sbLeftArm = new StringBuilder();
            StringBuilder sbRightArm = new StringBuilder();
            StringBuilder sbRotation = new StringBuilder();

            foreach (KeyValuePair<string, List<VMDFormat.Motion>> entry in dict)
            {
                if (entry.Key.Equals(headName))
                {
                    Console.WriteLine("reading data of " + entry.Key);
                    foreach(VMDFormat.Motion motion in entry.Value)
                    {
                        Vector3 vector = QuaternionToEuler(motion.rotation);
                        if (vector.X > 180) vector.X -= 360;
                        if (vector.Y > 180) vector.Y -= 360;
                        if (vector.Z > 180) vector.Z -= 360;
                        var x = Math.Floor(vector.X * 1000) / 1000;
                        var y = Math.Floor(vector.Y * 1000) / 1000;
                        var z = Math.Floor(vector.Z * 1000) / 1000;
                        string str = $"{Math.Floor(motion.frame_no / 1.5)}:{x},{y},{z}";

                        if (sbHead.Length == 0) sbHead.Append(str);
                        else sbHead.Append(";" + str);

                        if (sbHead.Length >= 1975)
                        {
                            headMoveList.Add(sbHead.ToString());
                            Console.WriteLine(sbHead.ToString());
                            sbHead.Length = 0;
                        }
                    }
                    if (sbHead.Length > 0) headMoveList.Add(sbHead.ToString());
                }
                if (entry.Key.Equals(bodyName))
                {
                    Console.WriteLine("reading data of " + entry.Key);
                    foreach (VMDFormat.Motion motion in entry.Value)
                    {
                        Vector3 vector = QuaternionToEuler(motion.rotation);
                        if (vector.X > 180) vector.X -= 360;
                        if (vector.Y > 180) vector.Y -= 360;
                        if (vector.Z > 180) vector.Z -= 360;
                        var x = Math.Floor(vector.X * 1000) / 1000;
                        var y = Math.Floor(vector.Y * 1000) / 1000;
                        var z = Math.Floor(vector.Z * 1000) / 1000;
                        string str = $"{Math.Floor(motion.frame_no / 1.5)}:{x},{y},{z}";

                        if (sbBody.Length == 0) sbBody.Append(str);
                        else sbBody.Append(";" + str);

                        if (sbBody.Length >= 1975)
                        {
                            bodyMoveList.Add(sbBody.ToString());
                            Console.WriteLine(sbBody.ToString());
                            sbBody.Length = 0;
                        }
                    }
                    if (sbBody.Length > 0) bodyMoveList.Add(sbBody.ToString());
                }
                if (entry.Key.Equals(leftLegName))
                {
                    Console.WriteLine("reading data of " + entry.Key);
                    foreach (VMDFormat.Motion motion in entry.Value)
                    {
                        Vector3 vector = QuaternionToEuler(motion.rotation);
                        if (vector.X > 180) vector.X -= 360;
                        if (vector.Y > 180) vector.Y -= 360;
                        if (vector.Z > 180) vector.Z -= 360;
                        var x = Math.Floor(vector.X * 1000) / 1000;
                        var y = Math.Floor(vector.Y * 1000) / 1000;
                        var z = Math.Floor(vector.Z * 1000) / 1000;
                        string str = $"{Math.Floor(motion.frame_no / 1.5)}:{x},{y},{z}";

                        if (sbLeftLeg.Length == 0) sbLeftLeg.Append(str);
                        else sbLeftLeg.Append(";" + str);

                        if (sbLeftLeg.Length >= 1975)
                        {
                            leftLegMoveList.Add(sbLeftLeg.ToString());
                            Console.WriteLine(sbLeftLeg.ToString());
                            sbLeftLeg.Length = 0;
                        }
                    }
                    if (sbLeftLeg.Length > 0) leftLegMoveList.Add(sbLeftLeg.ToString());
                }
                if (entry.Key.Equals(rightLegName))
                {
                    Console.WriteLine("reading data of " + entry.Key);
                    foreach (VMDFormat.Motion motion in entry.Value)
                    {
                        Vector3 vector = QuaternionToEuler(motion.rotation);
                        if (vector.X > 180) vector.X -= 360;
                        if (vector.Y > 180) vector.Y -= 360;
                        if (vector.Z > 180) vector.Z -= 360;
                        var x = Math.Floor(vector.X * 1000) / 1000;
                        var y = Math.Floor(vector.Y * 1000) / 1000;
                        var z = Math.Floor(vector.Z * 1000) / 1000;
                        string str = $"{Math.Floor(motion.frame_no / 1.5)}:{x},{y},{z}";

                        if (sbRightLeg.Length == 0) sbRightLeg.Append(str);
                        else sbRightLeg.Append(";" + str);

                        if (sbRightLeg.Length >= 1975)
                        {
                            rightLegMoveList.Add(sbRightLeg.ToString());
                            Console.WriteLine(sbRightLeg.ToString());
                            sbRightLeg.Length = 0;
                        }
                    }
                    if (sbRightLeg.Length > 0) rightLegMoveList.Add(sbRightLeg.ToString());
                }
                if (entry.Key.Equals(leftArmName))
                {
                    Console.WriteLine("reading data of " + entry.Key);
                    foreach (VMDFormat.Motion motion in entry.Value)
                    {
                        Vector3 vector = QuaternionToEuler(motion.rotation);
                        if (vector.X > 180) vector.X -= 360;
                        if (vector.Y > 180) vector.Y -= 360;
                        if (vector.Z > 180) vector.Z -= 360;
                        var x = Math.Floor(vector.X * 1000) / 1000;
                        var y = Math.Floor(vector.Y * 1000) / 1000;
                        var z = Math.Floor(vector.Z * 1000) / 1000;
                        string str = $"{Math.Floor(motion.frame_no / 1.5)}:{x},{y},{z}";

                        if (sbLeftArm.Length == 0) sbLeftArm.Append(str);
                        else sbLeftArm.Append(";" + str);

                        if (sbLeftArm.Length >= 1975)
                        {
                            leftArmMoveList.Add(sbLeftArm.ToString());
                            Console.WriteLine(sbLeftArm.ToString());
                            sbLeftArm.Length = 0;
                        }
                    }
                    if (sbLeftArm.Length > 0) leftArmMoveList.Add(sbLeftArm.ToString());
                }
                if (entry.Key.Equals(rightArmName))
                {
                    Console.WriteLine("reading data of " + entry.Key);
                    foreach (VMDFormat.Motion motion in entry.Value)
                    {
                        Vector3 vector = QuaternionToEuler(motion.rotation);
                        if (vector.X > 180) vector.X -= 360;
                        if (vector.Y > 180) vector.Y -= 360;
                        if (vector.Z > 180) vector.Z -= 360;
                        var x = Math.Floor(vector.X * 1000) / 1000;
                        var y = Math.Floor(vector.Y * 1000) / 1000;
                        var z = Math.Floor(vector.Z * 1000) / 1000;
                        string str = $"{Math.Floor(motion.frame_no / 1.5)}:{x},{y},{z}";

                        if (sbRightArm.Length == 0) sbRightArm.Append(str);
                        else sbRightArm.Append(";" + str);

                        if (sbRightArm.Length >= 1975)
                        {
                            rightArmMoveList.Add(sbRightArm.ToString());
                            Console.WriteLine(sbRightArm.ToString());
                            sbRightArm.Length = 0;
                        }
                    }
                    if (sbRightArm.Length > 0) rightArmMoveList.Add(sbRightArm.ToString());
                }
                if (entry.Key.Equals(rotationName))
                {
                    Console.WriteLine("reading data of " + entry.Key);
                    foreach (VMDFormat.Motion motion in entry.Value)
                    {
                        Vector3 vector = QuaternionToEuler(motion.rotation);
                        if (vector.X > 180) vector.X -= 360;
                        if (vector.Y > 180) vector.Y -= 360;
                        if (vector.Z > 180) vector.Z -= 360;
                        var x = Math.Floor(vector.X * 1000) / 1000;
                        var y = Math.Floor(vector.Y * 1000) / 1000;
                        var z = Math.Floor(vector.Z * 1000) / 1000;
                        string str = $"{Math.Floor(motion.frame_no / 1.5)}:{x},{y},{z}";

                        if (sbRotation.Length == 0) sbRotation.Append(str);
                        else sbRotation.Append(";" + str);

                        if (sbRightArm.Length >= 1975)
                        {
                            rotationMoveList.Add(sbRotation.ToString());
                            Console.WriteLine(sbRotation.ToString());
                            sbRotation.Length = 0;
                        }
                    }
                    if (sbRotation.Length > 0) rotationMoveList.Add(sbRotation.ToString());
                }
            }

            Console.WriteLine("");
            Console.WriteLine("generating code template");

            StringBuilder template = new StringBuilder("{\"blocks\":[{\"id\":\"block\",\"block\":\"func\",\"args\":{\"items\":[{\"item\":{\"id\":\"bl_tag\",\"data\":{\"option\":\"False\",\"tag\":\"Is Hidden\",\"action\":\"dynamic\",\"block\":\"func\"}},\"slot\":26}]},\"data\":\"" + filename + "\"},{\"id\":\"block\",\"block\":\"set_var\",\"args\":{\"items\":[{\"item\":{\"id\":\"var\",\"data\":{\"name\":\"motion_head\",\"scope\":\"local\"}},\"slot\":0}");

            //head
            int slot = 1;
            foreach (string str in headMoveList)
            {
                template.Append(",{\"item\":{\"id\":\"txt\",\"data\":{\"name\":\"" + str + "\",\"scope\":\"local\"}},\"slot\":" + slot + "}");
                if (slot < 26) slot++;
                else
                {
                    template.Append("]},\"action\":\"CreateList\"},{\"id\":\"block\",\"block\":\"set_var\",\"args\":{\"items\":[{\"item\":{\"id\":\"var\",\"data\":{\"name\":\"motion_head\",\"scope\":\"local\"}},\"slot\":0}");
                    slot = 1;
                }
            }
            template.Append("]},\"action\":\"CreateList\"}");

            //body
            template.Append(",{\"id\":\"block\",\"block\":\"set_var\",\"args\":{\"items\":[{\"item\":{\"id\":\"var\",\"data\":{\"name\":\"motion_body\",\"scope\":\"local\"}},\"slot\":0}");

            slot = 1;
            foreach (string str in bodyMoveList)
            {
                template.Append(",{\"item\":{\"id\":\"txt\",\"data\":{\"name\":\"" + str + "\",\"scope\":\"local\"}},\"slot\":" + slot + "}");
                if (slot < 26) slot++;
                else
                {
                    template.Append("]},\"action\":\"CreateList\"},{\"id\":\"block\",\"block\":\"set_var\",\"args\":{\"items\":[{\"item\":{\"id\":\"var\",\"data\":{\"name\":\"motion_body\",\"scope\":\"local\"}},\"slot\":0}");
                    slot = 1;
                }
            }
            template.Append("]},\"action\":\"CreateList\"}");

            //leftLeg
            template.Append(",{\"id\":\"block\",\"block\":\"set_var\",\"args\":{\"items\":[{\"item\":{\"id\":\"var\",\"data\":{\"name\":\"motion_leftLeg\",\"scope\":\"local\"}},\"slot\":0}");

            slot = 1;
            foreach (string str in leftLegMoveList)
            {
                template.Append(",{\"item\":{\"id\":\"txt\",\"data\":{\"name\":\"" + str + "\",\"scope\":\"local\"}},\"slot\":" + slot + "}");
                if (slot < 26) slot++;
                else
                {
                    template.Append("]},\"action\":\"CreateList\"},{\"id\":\"block\",\"block\":\"set_var\",\"args\":{\"items\":[{\"item\":{\"id\":\"var\",\"data\":{\"name\":\"motion_leftLeg\",\"scope\":\"local\"}},\"slot\":0}");
                    slot = 1;
                }
            }
            template.Append("]},\"action\":\"CreateList\"}");

            //rightLeg
            template.Append(",{\"id\":\"block\",\"block\":\"set_var\",\"args\":{\"items\":[{\"item\":{\"id\":\"var\",\"data\":{\"name\":\"motion_rightLeg\",\"scope\":\"local\"}},\"slot\":0}");

            slot = 1;
            foreach (string str in rightLegMoveList)
            {
                template.Append(",{\"item\":{\"id\":\"txt\",\"data\":{\"name\":\"" + str + "\",\"scope\":\"local\"}},\"slot\":" + slot + "}");
                if (slot < 26) slot++;
                else
                {
                    template.Append("]},\"action\":\"CreateList\"},{\"id\":\"block\",\"block\":\"set_var\",\"args\":{\"items\":[{\"item\":{\"id\":\"var\",\"data\":{\"name\":\"motion_rightLeg\",\"scope\":\"local\"}},\"slot\":0}");
                    slot = 1;
                }
            }
            template.Append("]},\"action\":\"CreateList\"}");

            //leftArm
            template.Append(",{\"id\":\"block\",\"block\":\"set_var\",\"args\":{\"items\":[{\"item\":{\"id\":\"var\",\"data\":{\"name\":\"motion_leftArm\",\"scope\":\"local\"}},\"slot\":0}");

            slot = 1;
            foreach (string str in leftArmMoveList)
            {
                template.Append(",{\"item\":{\"id\":\"txt\",\"data\":{\"name\":\"" + str + "\",\"scope\":\"local\"}},\"slot\":" + slot + "}");
                if (slot < 26) slot++;
                else
                {
                    template.Append("]},\"action\":\"CreateList\"},{\"id\":\"block\",\"block\":\"set_var\",\"args\":{\"items\":[{\"item\":{\"id\":\"var\",\"data\":{\"name\":\"motion_leftArm\",\"scope\":\"local\"}},\"slot\":0}");
                    slot = 1;
                }
            }
            template.Append("]},\"action\":\"CreateList\"}");

            //rightArm
            template.Append(",{\"id\":\"block\",\"block\":\"set_var\",\"args\":{\"items\":[{\"item\":{\"id\":\"var\",\"data\":{\"name\":\"motion_rightArm\",\"scope\":\"local\"}},\"slot\":0}");

            slot = 1;
            foreach (string str in rightArmMoveList)
            {
                template.Append(",{\"item\":{\"id\":\"txt\",\"data\":{\"name\":\"" + str + "\",\"scope\":\"local\"}},\"slot\":" + slot + "}");
                if (slot < 26) slot++;
                else
                {
                    template.Append("]},\"action\":\"CreateList\"},{\"id\":\"block\",\"block\":\"set_var\",\"args\":{\"items\":[{\"item\":{\"id\":\"var\",\"data\":{\"name\":\"motion_rightArm\",\"scope\":\"local\"}},\"slot\":0}");
                    slot = 1;
                }
            }
            template.Append("]},\"action\":\"CreateList\"}");

            //rotation
            template.Append(",{\"id\":\"block\",\"block\":\"set_var\",\"args\":{\"items\":[{\"item\":{\"id\":\"var\",\"data\":{\"name\":\"motion_rotation\",\"scope\":\"local\"}},\"slot\":0}");

            slot = 1;
            foreach (string str in rotationMoveList)
            {
                template.Append(",{\"item\":{\"id\":\"txt\",\"data\":{\"name\":\"" + str + "\",\"scope\":\"local\"}},\"slot\":" + slot + "}");
                if (slot < 26) slot++;
                else
                {
                    template.Append("]},\"action\":\"CreateList\"},{\"id\":\"block\",\"block\":\"set_var\",\"args\":{\"items\":[{\"item\":{\"id\":\"var\",\"data\":{\"name\":\"motion_rotation\",\"scope\":\"local\"}},\"slot\":0}");
                    slot = 1;
                }
            }
            template.Append("]},\"action\":\"CreateList\"}]}");

            Console.WriteLine(template.ToString());

            IPAddress host = IPAddress.Parse("127.0.0.1");
            int port = 31372;

            IPEndPoint ipe = new IPEndPoint(host, port);
            var client = new TcpClient();
            client.Connect(ipe);

            string templateData = ToGZip(template.ToString());
            Console.WriteLine(templateData);
            Console.WriteLine("");
            Console.WriteLine("sending template through socket (make sure you have codeutilities installed)");

            using (var stream = client.GetStream())
            {
                string json = "{\"type\":\"template\",\"source\":\"vmd2df\",\"data\":\"{\\\"name\\\":\\\"" + filename + "\\\",\\\"data\\\":\\\"" + templateData + "\\\"}\"}";
                var buffer = Encoding.UTF8.GetBytes(json);
                stream.Write(buffer, 0, buffer.Length);
            }

            client.Dispose();
            Console.WriteLine("transfer done");
        }

        public static Vector3 QuaternionToEuler(Quaternion q1)
        {
            float sqw = q1.W * q1.W;
            float sqx = q1.X * q1.X;
            float sqy = q1.Y * q1.Y;
            float sqz = q1.Z * q1.Z;
            float unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
            float test = q1.X * q1.W - q1.Y * q1.Z;
            Vector3 v;

            if (test > 0.4995f * unit)
            { // singularity at north pole
                v.Y = 2f * MathF.Atan2(q1.Y, q1.X);
                v.X = MathF.PI / 2;
                v.Z = 0;
                return NormalizeAngles(v * 360 / (MathF.PI * 2));
            }
            if (test < -0.4995f * unit)
            { // singularity at south pole
                v.Y = -2f * MathF.Atan2(q1.Y, q1.X);
                v.X = -MathF.PI / 2;
                v.Z = 0;
                return NormalizeAngles(v * 360 / (MathF.PI * 2));
            }
            Quaternion q = new Quaternion(q1.W, q1.Z, q1.X, q1.Y);
            v.Y = (float)Math.Atan2(2f * q.X * q.W + 2f * q.Y * q.Z, 1 - 2f * (q.Z * q.Z + q.W * q.W));     // Yaw
            v.X = (float)Math.Asin(2f * (q.X * q.Z - q.W * q.Y));                             // Pitch
            v.Z = (float)Math.Atan2(2f * q.X * q.Y + 2f * q.Z * q.W, 1 - 2f * (q.Y * q.Y + q.Z * q.Z));      // Roll
            return NormalizeAngles(v * 360 / (MathF.PI * 2));
        }

        static Vector3 NormalizeAngles(Vector3 angles)
        {
            angles.X = NormalizeAngle(angles.X);
            angles.Y = NormalizeAngle(angles.Y);
            angles.Z = NormalizeAngle(angles.Z);
            return angles;
        }

        static float NormalizeAngle(float angle)
        {
            while (angle > 360)
                angle -= 360;
            while (angle < 0)
                angle += 360;
            return angle;
        }

        public double ToAngle(double radian)
        {
            return (double)(radian * 180 / Math.PI);
        }

        public static string ToGZip(string inputStr)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputStr);

            using (var outputStream = new MemoryStream())
            {
                using (var gZipStream = new GZipStream(outputStream, CompressionMode.Compress))
                    gZipStream.Write(inputBytes, 0, inputBytes.Length);

                var outputBytes = outputStream.ToArray();

                var outputStr = Convert.ToBase64String(outputBytes);

                return outputStr;
            }
        }
    }
}
