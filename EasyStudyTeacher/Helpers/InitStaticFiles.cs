﻿using LaptopShop.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStudy.Helpers
{
    public static class InitStaticFiles
    {
        public static string CreateFolderServer(IWebHostEnvironment env,
            IConfiguration configuration, string[] settings)
        {
            string fileDestDir = env.ContentRootPath;
            foreach (var pathConfig in settings)
            {
                fileDestDir = Path.Combine(fileDestDir, configuration.GetValue<string>(pathConfig));
                if (!Directory.Exists(fileDestDir))
                {
                    Directory.CreateDirectory(fileDestDir);
                }
            }
            return fileDestDir;
        }

        public static string CreateImageByFileName(IWebHostEnvironment env,
                                                  IConfiguration configuration,
                                                  string[] settingsFolder,
                                                  string fileName, string base64)
        {
            string[] imageSizes = ((string)configuration.GetValue<string>("ImageSizes")).Split(" ");
            bool fileBeginCreated = false;
            string fileDestDir = env.ContentRootPath;

            try
            {
                foreach (var pathConfig in settingsFolder)
                {
                    fileDestDir = Path.Combine(fileDestDir, pathConfig);
                    if (!Directory.Exists(fileDestDir))
                    {
                        Directory.CreateDirectory(fileDestDir);
                    }
                }

                fileBeginCreated = true;

                if (base64.Contains(","))
                {
                    base64 = base64.Split(',')[1];
                }
                using (var bmp = base64.FromBase64StringToImage())
                {
                    foreach (var imagePrefix in imageSizes)
                    {
                        int size = int.Parse(imagePrefix);
                        string fileSave = Path.Combine(fileDestDir, $"{imagePrefix}_{fileName}");
                        if (bmp != null)
                        {
                            using (var image = ImageHelper.CompressImage(bmp, size, size))
                            {
                                if (image == null)
                                    throw new Exception("В процесі створення фото виникли проблеми");

                                image.Save(fileSave, ImageFormat.Jpeg);

                            }
                        }
                        else
                        {
                            throw new Exception("В процесі створення фото виникли проблеми");
                        }
                    }
                }
            }
            catch
            {
                if (fileBeginCreated)
                {
                    foreach (var imagePrefix in imageSizes)
                    {
                        string fileImage = Path.Combine(fileDestDir, $"{imagePrefix}_{fileName}");
                        if (File.Exists(fileImage))
                        {
                            File.Delete(fileImage);
                        }
                    }
                }
                return null;
            }
            return fileDestDir;
        }

        public static string CreateImageByFileName(IWebHostEnvironment env,
                                                  IConfiguration configuration,
                                                  string[] settingsFolder,
                                                  string fileName, string base64, string base64Big)
        {
            string[] imageSizes = ((string)configuration.GetValue<string>("ImageSizes")).Split(" ");
            bool fileBeginCreated = false;
            string fileDestDir = env.ContentRootPath;

            try
            {
                foreach (var pathConfig in settingsFolder)
                {
                    fileDestDir = Path.Combine(fileDestDir, configuration.GetValue<string>(pathConfig));
                    if (!Directory.Exists(fileDestDir))
                    {
                        Directory.CreateDirectory(fileDestDir);
                    }
                }

                fileBeginCreated = true;

                if (base64.Contains(","))
                {
                    base64 = base64.Split(',')[1];
                }
                using (var bmp = base64.FromBase64StringToImage())
                {
                    for (int i = 0; i < imageSizes.Length - 1; i++)
                    {
                        var imagePrefix = imageSizes[i];
                        int size = int.Parse(imagePrefix);
                        string fileSave = Path.Combine(fileDestDir, $"{imagePrefix}_{fileName}");
                        if (bmp != null)
                        {
                            using (var image = ImageHelper.CompressImage(bmp, size, size))
                            {
                                if (image == null)
                                    throw new Exception("В процесі створення фото виникли проблеми");

                                image.Save(fileSave, ImageFormat.Jpeg);

                            }
                        }
                        else
                        {
                            throw new Exception("В процесі створення фото виникли проблеми");
                        }
                    }
                }

                if (base64Big.Contains(","))
                {
                    base64Big = base64Big.Split(',')[1];
                }
                using (var bmp = base64Big.FromBase64StringToImage())
                {
                    for (int i = imageSizes.Length - 1; i < imageSizes.Length; i++)
                    {
                        var imagePrefix = imageSizes[i];
                        int size = int.Parse(imagePrefix);
                        string fileSave = Path.Combine(fileDestDir, $"{imagePrefix}_{fileName}");
                        if (bmp != null)
                        {
                            using (var image = ImageHelper.CompressImage(bmp, size, size))
                            {
                                if (image == null)
                                    throw new Exception("В процесі створення фото виникли проблеми");

                                image.Save(fileSave, ImageFormat.Jpeg);
                            }
                        }
                        else
                        {
                            throw new Exception("В процесі створення фото виникли проблеми");
                        }
                    }
                }
            }
            catch
            {
                if (fileBeginCreated)
                {
                    foreach (var imagePrefix in imageSizes)
                    {
                        string fileImage = Path.Combine(fileDestDir, $"{imagePrefix}_{fileName}");
                        if (File.Exists(fileImage))
                        {
                            File.Delete(fileImage);
                        }
                    }
                }
                return null;
            }
            return fileDestDir;
        }
        public static void DeleteImageByFileName(IWebHostEnvironment env,
                                                  IConfiguration configuration,
                                                  string[] settingsFolder,
                                                  string fileName)
        {
            string[] imageSizes = ((string)configuration.GetValue<string>("ImageSizes")).Split(" ");
            //bool fileBeginCreated = false;
            string fileDestDir = env.ContentRootPath;

            foreach (var pathConfig in settingsFolder)
            {
                fileDestDir = Path.Combine(fileDestDir, configuration.GetValue<string>(pathConfig));
                if (!Directory.Exists(fileDestDir))
                {
                    Directory.CreateDirectory(fileDestDir);
                }
            }
            foreach (var imagePrefix in imageSizes)
            {
                string fileImage = Path.Combine(fileDestDir, $"{imagePrefix}_{fileName}");
                if (File.Exists(fileImage))
                {
                    File.Delete(fileImage);
                }
            }
        }
    }
}
