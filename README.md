# Shader Almighty

## Features

### [FormerlySerializedAs()] for shader

This attribute in shader file will be a rule to re-serialize your materials that uses the shader. It will read the values from the filesystem in `*.mat`, and put the values at the property of the new name. 

#### Example

```shaderlab
Shader "Custom/UI/Default"
{
    Properties
    {
        [FormerlySerializedAs(_PrevValue)] _ChangedValue ("Changed Value", Float) = 0
        ...
    }
    ...
}
```



`[FormerlySerializedAs(OLD_NAME)]` property can specify what you want to re-serialize the materials on postprocess timing from the callback ' `AssetPostprocessor`'. This works likes [FormerlySerializedAs](https://docs.unity3d.com/ScriptReference/Serialization.FormerlySerializedAsAttribute.html) in MonoScript API. 



## TODO

- Replacement tool
- Clean up unused property against binded shader.



## Install

### hosted by npm.js

```json
{
    "dependencies": {
        "kr.seonghwan.shader-almighty": "0.0.2"
    }
}
```

```json
{
    "scopedRegistries": [
        {
            "name": "npm-seonghwan",
            "url": "https://registry.npmjs.org",
            "scopes": [
                "kr.seonghwan",
                "aaubry"
            ]
        }
    ]
}
```



## Dependencies

- [YAML dotnet](https://github.com/aaubry/YamlDotNet) from [Antoine Aubry](https://github.com/aaubry)
