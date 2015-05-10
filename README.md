ObjLoader
========

Objloader is a simple Wavefront .obj and .mtl loader

Installation 
------------
Build the project and reference the .dll or reference the project directly as usual.

Loading a model
---------------
Either create the loader with the standard material stream provider, this will open the file read-only from the working directory.

	var objLoaderFactory = new ObjLoaderFactory();
	var objLoader = objLoaderFactory.Create();

    
Or provide your own:

    //With the signature Func<string, Stream>
    var objLoaderFactory = new ObjLoaderFactory();
    var objLoader = objLoaderFactory.Create(materialFileName => File.Open(materialFileName);

Then it is just a matter of invoking the loader with a stream containing the model. 

    var fileStream = new FileStream("model.obj");
    var result = objLoader.Load(fileStream);

The result object contains the loaded model in this form:
	
    public class LoadResult  
    {
        public IList<Vertex> Vertices { get; set; }
        public IList<Texture> Textures { get; set; }
        public IList<Normal> Normals { get; set; }
        public IList<Group> Groups { get; set; }
        public IList<Material> Materials { get; set; }
    }
    
License
-------
The MIT License (MIT)

Copyright (c) 2015 Chris Jansson

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
