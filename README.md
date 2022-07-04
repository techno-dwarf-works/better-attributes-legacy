# Better Attributes

This package provides following features:

### Select Implementation 
Provides possibility to select interface implementation in Unity inspector.

Usage:
```c#
[SelectImplementation] [SerealizeReference] 
private ISomeInterface someInterface;
```

### Editor Buttons

Provides possibility to display button for method in Unity inspector.

Usages:
```c#
///Default usage of attribute.
[EditorButton]
private void SomeMethod()
{
//Some code.
}

///This button will call method with predefined parameters. 
///When invokeParams not specified will call with null.
[EditorButton(invokeParams: 10f)]
private void SomeMethod(float floatValue)
{
//Some code.
}

///This button will call method with predefined parameters. 
///When invokeParams not specified will call with null.
[EditorButton(invokeParams: new object[] { 10f, 10 })]
private void SomeMethod(float floatValue, int intValue)
{
//Some code.
}

/// This button will be in the same row with button for SomeMethod2.
/// But will be in the second position.
/// When captureGroup not specified each button placed in separate row.
/// When priority not specified buttons in one row sorted by order in code.
[EditorButton(captureGroup: 1, priority: 2)]
private void SomeMethod1()
{
//Some code.
}

[EditorButton(captureGroup: 1, priority: 1)]
private void SomeMethod2()
{
//Some code.
}

/// This button will have name "Some Cool Button".
/// When displayName not specified or null/empty/whitespace button 
/// will have name same as method.
[EditorButton(displayName: "Some Cool Button")]
private void SomeMethod()
{
//Some code
}
```

You can check constructors for `EditorButtonAttribute` there more specific options.

## TODOs:

1. SerializedType;
2. Fix that buttons not working if there another custom editor defined;
3. Improve `SelectImplementation` drawer;

