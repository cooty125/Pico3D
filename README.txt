Pico 3D
Embedded 3D graphics library for Windows (.NET)
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
	
# Release (32-bit)
	
	# Output library file
	./release_x86/pico3d_ref.dll
	
	# Compiled shaders
	./release_x86/default.psf
	./release_x86/texture.psf
	
#Other files
	./logo.png

------------------------------------------------------------------------------------------------
Copyright © David Kutnar 2016 - All rights reserved!