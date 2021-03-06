using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;


[RealtimeModel(createMetaModel: true)]
public partial class ScaleSyncModel 
{
    [RealtimeProperty(1, false, true)]
    private float _scale;
    
}

/* ----- Begin Normal Autogenerated Code ----- */
public partial class ScaleSyncModel : RealtimeModel {
    public float scale {
        get {
            return _scaleProperty.value;
        }
        set {
            if (_scaleProperty.value == value) return;
            _scaleProperty.value = value;
            InvalidateUnreliableLength();
            FireScaleDidChange(value);
        }
    }
    
    public delegate void PropertyChangedHandler<in T>(ScaleSyncModel model, T value);
    public event PropertyChangedHandler<float> scaleDidChange;
    
    public enum PropertyID : uint {
        Scale = 1,
    }
    
    #region Properties
    
    private UnreliableProperty<float> _scaleProperty;
    
    #endregion
    
    public ScaleSyncModel() : base(new MetaModel()) {
        _scaleProperty = new UnreliableProperty<float>(1, _scale);
    }
    
    private void FireScaleDidChange(float value) {
        try {
            scaleDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    protected override int WriteLength(StreamContext context) {
        var length = MetaModelWriteLength(context);
        length += _scaleProperty.WriteLength(context);
        return length;
    }
    
    protected override void Write(WriteStream stream, StreamContext context) {
        WriteMetaModel(stream, context);
        
        var writes = false;
        writes |= _scaleProperty.Write(stream, context);
        if (writes) InvalidateContextLength(context);
    }
    
    protected override void Read(ReadStream stream, StreamContext context) {
        var anyPropertiesChanged = false;
        while (stream.ReadNextPropertyID(out uint propertyID)) {
            var changed = false;
            switch (propertyID) {
                case MetaModel.ReservedPropertyID: {
                    ReadMetaModel(stream, context);
                    break;
                }
                case (uint) PropertyID.Scale: {
                    changed = _scaleProperty.Read(stream, context);
                    if (changed) FireScaleDidChange(scale);
                    break;
                }
                default: {
                    stream.SkipProperty();
                    break;
                }
            }
            anyPropertiesChanged |= changed;
        }
        if (anyPropertiesChanged) {
            UpdateBackingFields();
        }
    }
    
    private void UpdateBackingFields() {
        _scale = scale;
    }
    
}
/* ----- End Normal Autogenerated Code ----- */
