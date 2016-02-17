Pico 3D
Embedded 3D graphics library for Windows (.NET)
Last Update: 17.2 2016
------------------------------------------------------------------------------------------------
Read LICENSE file first!
./LICENSE

# Version
0.0.9.7
	
#References
	
	#.NET 4.0 Client Profile
	./refs/Microsoft.Xna.Framework.Pipeline.dll
	./refs/Microsoft.Xna.Framework.Pipeline.EffectImporter.dll
	./refs/Microsoft.Xna.Framework.dll
	./refs/Microsoft.Xna.Framework.Graphics.dll
	
# Source code

	# Solution file (Visual Studio 2012 Desktop) 
	./src/Pico 3D.sln
	
	# Project file
	./src/Pico 3D.csproj

	# Source files
	./src/
	./src/Embedded/
	./src/Embedded/EmbedPico3D.cs
	./src/Embedded/GraphicsDeviceService.cs
	./src/Graphics/
	./src/Graphics/Material.cs
	./src/Graphics/Mesh.cs
	./src/Graphics/Model.cs
	./src/Graphics/ModelMath.cs
	./src/Graphics/Shader.cs
	./src/Graphics/Vertex.cs
	./src/Importers/
	./src/Importers/MOFImporter.cs
	./src/Primitives/
	./src/Primitives/BoundingBox.cs
	./src/Properties/
	./src/Properties/Assembly.cs
	./src/Renderers/
	./src/Renderers/BasicRenderer.cs
	./src/Renderers/VertexRenderer.cs
	./src/Shaders/
	./src/Shaders/default.fx
	./src/Shaders/texture.fx
	./src/Constants.cs
	./src/IImporter.cs
	./src/IRenderer.cs
	./src/ServiceContainer.cs
	./src/icon.ico
	
# Pico 3D Release (32-bit)
	
	# Output library file
	./release_x86/pico3d_ref.dll
	
	# Compiled shaders
	./release_x86/default.psf
	./release_x86/texture.psf
	
#Tools

	# FX Compiler (shader compiler)
	./tools/FX Compiler/
	./tools/FX Compiler/src/
	./tools/FX Compiler/src/Properties/
	./tools/FX Compiler/src/Properties/Assembly.cs
	./tools/FX Compiler/src/CustomImporterContext.cs
	./tools/FX Compiler/src/CustomProcessorContext.cs
	./tools/FX Compiler/src/FX Compiler.csproj
	./tools/FX Compiler/src/FXCompiler.cs
	
	#Release (32-bit)
	./tools/FX Compiler/release_x86/fx_comp.exe 
	
	#Usage
	fx_comp.exe <input shader file> <output shader file>
	fx_comp.exe "myShader.fx" "myShader.psf"
	
	#OBJ2PMF (model converter)
	./tools/OBJ2PMF/
	./tools/OBJ2PMF/Cinema 4D OBJ Export Script/
	./tools/OBJ2PMF/Cinema 4D OBJ Export Script/C4D OBJ Exporter.py
	./tools/OBJ2PMF/Cinema 4D OBJ Export Script/C4D OBJ Exporter.tif
	./tools/OBJ2PMF/src/
	./tools/OBJ2PMF/src/Properties/
	./tools/OBJ2PMF/src/Properties/Assembly.cs
	./tools/OBJ2PMF/src/Mesh.cs
	./tools/OBJ2PMF/src/OBJ2PMF.cs
	
	#Release (32-bit)
	./tools/OBJ 2 PMF/release_x86/obj_2_pmf.exe
	
	#Cinema4D OBJ Export Script
	- Copy these files:
	./tools/OBJ2PMF/Cinema 4D OBJ Export Script/C4D OBJ Exporter.py
	./tools/OBJ2PMF/Cinema 4D OBJ Export Script/C4D OBJ Exporter.tif
	- Here:
	%appdata%\MAXON\Cinema 4D R<your c4d version>\library\scripts\
	
	#Usage
	- Model without materials:
	obj_2_pmf.exe <input model file> <output model file>
	- Model with materials:
	obj_2_pmf.exe <input model file> <output model file> /d <diffuse map texture> /n <normal map texture> /s <specular map texture> /e <emissive map texture>
	
#Other files

	./logo.png

------------------------------------------------------------------------------------------------
Copyright © David Kutnar 2016 - All rights reserved!
