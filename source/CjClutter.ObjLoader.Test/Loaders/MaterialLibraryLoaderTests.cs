using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using ObjLoader.Loader.Data;
using System.Linq;
using FluentAssertions;
using ObjLoader.Loader.Data.DataStore;
using ObjLoader.Loader.Loaders;

namespace ObjLoader.Test.Loaders
{
    [TestFixture]
    public class MaterialLibraryLoaderTests
    {
        private MaterialLibrarySpy _materialLibrarySpy;
        private Material _firstMaterial;
        private Material _secondMaterial;
        private const float Epsilon = 0.000001f;
        
        private MaterialLibraryLoader _materialLibraryLoader;

        [SetUp]
        public void SetUp()
        {
            _materialLibrarySpy = new MaterialLibrarySpy();
            _materialLibraryLoader = new MaterialLibraryLoader(_materialLibrarySpy);
        }

        [Test]
        public void Adds_correct_materials()
        {
            LoadMaterial();
            
            _materialLibrarySpy.Materials.Count().Should().Be(2);
            _firstMaterial.Name.Should().BeEquivalentTo("wire_134110008");
            _secondMaterial.Name.Should().BeEquivalentTo("second_material");
        }

        [Test]
        public void Sets_correct_ambient_color()
        {
            LoadMaterial();

            _firstMaterial.AmbientColor.X.Should().BeApproximately(1, Epsilon);
            _firstMaterial.AmbientColor.Y.Should().BeApproximately(2, Epsilon);
            _firstMaterial.AmbientColor.Z.Should().BeApproximately(3, Epsilon);
        }

        [Test]
        public void Sets_correct_diffuse_color()
        {
            LoadMaterial();

            _firstMaterial.DiffuseColor.X.Should().BeApproximately(0.5255f, Epsilon);
            _firstMaterial.DiffuseColor.Y.Should().BeApproximately(0.4314f, Epsilon);
            _firstMaterial.DiffuseColor.Z.Should().BeApproximately(0.0314f, Epsilon);
        }

        [Test]
        public void Sets_correct_specular()
        {
            LoadMaterial();

            _firstMaterial.SpecularColor.X.Should().BeApproximately(0.3500f, Epsilon);
            _firstMaterial.SpecularColor.Y.Should().BeApproximately(0.3600f, Epsilon);
            _firstMaterial.SpecularColor.Z.Should().BeApproximately(0.3700f, Epsilon);

            _firstMaterial.SpecularCoefficient.Should().BeApproximately(32, Epsilon);
        }

        [Test]
        public void Sets_correct_transparency()
        {
            LoadMaterial();

            _firstMaterial.Transparency.Should().BeApproximately(0.5f, Epsilon);
        }

        [Test]
        public void Sets_correct_illumination_model()
        {
            LoadMaterial();

            _firstMaterial.IlluminationModel.Should().Be(2);
        }

        [Test]
        public void Sets_correct_texure_maps()
        {
            LoadMaterial();

            _firstMaterial.AmbientTextureMap.Should().BeEquivalentTo("lenna1.tga");
            _firstMaterial.DiffuseTextureMap.Should().BeEquivalentTo("lenna2.tga");
            _firstMaterial.SpecularTextureMap.Should().BeEquivalentTo("lenna3.tga");
            _firstMaterial.SpecularHighlightTextureMap.Should().BeEquivalentTo("lenna_spec.tga");
            _firstMaterial.AlphaTextureMap.Should().BeEquivalentTo("lenna_alpha.tga");
            _firstMaterial.BumpMap.Should().BeEquivalentTo("lenna_bump.tga");
            _firstMaterial.DisplacementMap.Should().BeEquivalentTo("lenna_disp.tga");
            _firstMaterial.StencilDecalMap.Should().BeEquivalentTo("lenna_stencil.tga");
        }

        private void LoadMaterial()
        {
            var data = Encoding.ASCII.GetBytes(MaterialLibrary);
            var materialStream = new MemoryStream(data);

            _materialLibraryLoader.Load(materialStream);
            _firstMaterial = _materialLibrarySpy.Materials.First();
            _secondMaterial = _materialLibrarySpy.Materials.ElementAt(1);
        }

        private const string MaterialLibrary = 
    "newmtl wire_134110008\r\n" +
"\tNs\t32\r\n" +
@"Tr 0.5
illum 2
Ka 1.0000 2.0000 3.0000
Kd 0.5255 0.4314 0.0314
Ks 0.3500 0.3600 0.3700
map_Ka lenna1.tga
map_Kd lenna2.tga
map_Ks lenna3.tga
map_Ns lenna_spec.tga
map_d lenna_alpha.tga
map_bump lenna_bump.tga
disp lenna_disp.tga
decal lenna_stencil.tga
newmtl second_material";

        private class MaterialLibrarySpy : IMaterialLibrary
        {
            public List<Material> Materials { get; private set; }

            public MaterialLibrarySpy()
            {
                Materials = new List<Material>();
            }

            public void Push(Material material)
            {
                Materials.Add(material);
            }
        }
    }
}