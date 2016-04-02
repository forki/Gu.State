# Gu.State
Library for managing state.

## Table of Contents
- [1. EqualBy](#1-equalby)
- [1.1. FieldValues](#11-fieldValues)
- [1.2. PropertyValues](#12-propertyValues)
- [2. Copy](#2-copy)
- [2.1. FieldValues](#21-fieldValues)
- [2.2. PropertyValues](#22-propertyValues)

## 1. EqualBy
Compares two instances.

- Types implementing `IEquatable` are compared using `object.Equals(x, y)`
- Indexers are only supported for framework collection types like `List<T>`.
- Handles enumerables.

### 1.1. FieldValues
```
EqualBy.FieldValues(x, y);
EqualBy.FieldValues(x, y, ReferenceHandling.Structural); 
EqualBy.FieldValues(x, y, ReferenceHandling.StructuralWithReferenceLoops); 
EqualBy.FieldValues(x, y, ReferenceHandling.References);
```

- Ignores event fields

### 1.2. PropertyValues
```
EqualBy.PropertyValues(x, y);
EqualBy.PropertyValues(x, y, ReferenceHandling.Structural); 
EqualBy.PropertyValues(x, y, ReferenceHandling.StructuralWithReferenceLoops); 
EqualBy.PropertyValues(x, y, ReferenceHandling.References);
```

## 2. Copy
Copies values from source to target.
Immutable types are copied by value/reference.
- Indexers are only supported for framework collection types.
- Collections must implement `IList` or `IDictionary`

### 2.1. FieldValues
```
Copy.FieldValues(source, target);
Copy.FieldValues(source, target, ReferenceHandling.Structural); 
Copy.FieldValues(source, target, ReferenceHandling.StructuralWithReferenceLoops); 
Copy.FieldValues(source, target, ReferenceHandling.References);
```
### 2.2. PropertyValues
```
Copy.PropertyValues(source, target);
Copy.PropertyValues(source, target, ReferenceHandling.Structural); 
Copy.PropertyValues(source, target, ReferenceHandling.StructuralWithReferenceLoops); 
Copy.PropertyValues(source, target, ReferenceHandling.References);
```
##### FieldsSettings.
For more finegrained control there is an overload accepting a `FieldsSettings`


##### PropertiesSettings.
For more finegrained control there is an overload accepting a `PropertiesSettings`


## DiffBy
Compares two instances and returns a tree with the diff or null is they are equal.
Types implementing `IEquatable` are compared using `object.Equals(x, y)`
- Indexers are only supported for framework collection types.
- Handles enumerables.

#### FieldValues
```
DiffBy.FieldValues(x, y);
DiffBy.FieldValues(x, y, ReferenceHandling.Structural); 
DiffBy.FieldValues(x, y, ReferenceHandling.StructuralWithReferenceLoops); 
DiffBy.FieldValues(x, y, ReferenceHandling.References);
```
- Ignores event fields

#### PropertyValues
```
DiffBy.PropertyValues(x, y);
DiffBy.PropertyValues(x, y, ReferenceHandling.Structural); 
DiffBy.PropertyValues(x, y, ReferenceHandling.StructuralWithReferenceLoops); 
DiffBy.PropertyValues(x, y, ReferenceHandling.References);
```

## Track
Tracks changes in a graph.
For subproperties the following must hold:
- Collections must implement INotifyCollectionChanged
- Types that are not collections and not immutable must implement INotifyPropertyChanged.
- Indexers are only supported for framework collection types.

##### Changes.

```
using (var tracker = Track.Changes(foo))
{
    Assert.AreEqual(0, tracker.Changes);
    foo.SomeProperty.NestedCollection[0].Value++;
    Assert.AreEqual(1, tracker.Changes);
}
// no longer tracking after disposing.
```

##### IsDirty.

```
using (var tracker = Track.IsDirty(x, y))
{
    Assert.AreEqual(false, tracker.IsDirty);
    foo.SomeProperty.NestedCollection[0].Value++;
    Assert.AreEqual(true, tracker.IsDirty);
}
// no longer tracking after disposing.
```

##### Verify.
```
Track.VerifyCanTrackIsDirty<T>(ReferenceHandling.Structural);
Track.VerifyCanTrackChanges<T>(ReferenceHandling.Structural);
```
Use the verify methods in unit tests to assert that your types support tracking and that the correct settings are used.

##### PropertiesSettings.
For more finegrained control there is an overload accepting a `PropertiesSettings`
```
var settings = new PropertiesSettingsBuilder().IgnoreProperty<WithIllegal>(x => x.Illegal)
                                              .CreateSettings(ReferenceHandling.Structural);
using (var tracker = Track.Changes(withIllegalObject, settings))
{
    ...
}
```

## Synchronize
Keeps the property values of target in sync with source.
For subproperties the following must hold:
- Collections must implement INotifyCollectionChanged
- Types that are not collections and not immutable must implement INotifyPropertyChanged.
- Indexers are only supported for framework collection types.
```
using (Synchronize.CreatePropertySynchronizer(source, target, referenceHandling: ReferenceHandling.Structural))
{
    ...
}
```
