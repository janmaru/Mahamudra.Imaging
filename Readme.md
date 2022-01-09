﻿# Imaging
![alt text](johannes-plenio-NVMF-cAHxCg-unsplash.jpg "Mahamudra Cryptography OneKeyPad")
[Photo by Johannes Plenio on Unsplash]

A simple class that explores the manipulation of images on .net core with SixLabors.ImageSharp.

## Usage

```c#
        public static async Task<Image> Resize(Picture picture)
        {
            await picture.Resize(new Size(512, 512));
            return picture.GetImage();
        } 

        public static async Task<Image> Matrix(Picture picture, Rgba32 red, Rgba32 green, Rgba32 blue)
        {
            var matrix  = new ColorMatrix().ToColorMatrix(red: red, green: green, blue: blue);
            await picture.Filter(matrix);
            return picture.GetImage();
        }

        public static async Task<Image> Compose(List<Picture> pics)
        {
            var root = pics.First().GetImage();
            for (int i = 0; i < pics.Count; i++)
            {
                await pics[i].Resize(new Size(512, 512));
                var imageLayer = pics[i].GetImage();
                root.Mutate(o => o
                   .DrawImage(imageLayer, 1f)
                );
            }
            return root;
        }
```
In order the above samples:

1. **Resize** picture to 512 of width
2. Apply a **color matrix** playing with red, green, and blue(s)
3. **Compose** two different pictures by blending their pixels.

The idea behind this project is to keep an image "inside" the class to process it multiple times. 
Async/Await approach is implemented to free thread(s) when the application is run, for instance, on a web server.
There has been plenty of talk about ThreadPool starvation in .NET, which can happen when there is a lot of waiting synchronously on a task.
```c#
        await Task.Run(() => /* your code here*/);
```
Task run can mitigate somehow the problem, but image processing is high CPU or GPU bound, and from a user-code perspective, unfortunately, there is not much more to be done.
If the code is run on AWS or another cloud it is better to choose Lambda(s).
A compute service that lets run code without provisioning or managing servers on a high-availability compute infrastructure.


 