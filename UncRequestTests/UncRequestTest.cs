﻿using System;
using System.IO;
using System.Threading;
using CnDCopy.Kernel;
using CnDCopy.Kernel.LocationManagers.UNC;
using NUnit.Framework;

namespace UncRequestTests
{
    [TestFixture]
    public class UncRequestTest
    {
        private ManualResetEventSlim _mre;

        [Test]
        public void GetFileDownload()
        {
            _mre = new ManualResetEventSlim(false);
            var locationManager = new UncLocationManager();
            var location = new UncLocation
            {
                Credentials = new Credentials { Username = "test" },
                ItemUri = new Uri(@"c:\test.pdf"),
            };
            int bytesRead = 0;
            using (var sw = new BinaryWriter(File.Create(@"c:\test2.pdf")))
            {
                locationManager.BeginRetreiveFile(location, buffer =>
                                                                {
                                                                    sw.Write(buffer);
                                                                    bytesRead += buffer.Length;
                                                                }, () => _mre.Set());
                _mre.Wait();
                sw.Flush();
                sw.Close();
            }
            Assert.AreEqual(129781, bytesRead);
        }
    }
}