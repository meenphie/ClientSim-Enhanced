using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;


[assembly: UxmlNamespacePrefix("VRC.SDKBase.Editor.Elements", "vrc")]
namespace VRC.SDKBase.Editor.Elements
{
    public class Modal : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<Modal, UxmlTraits> {}
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription _title = new() { name = "title" };
            
            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get
                {
                    yield return new UxmlChildElementDescription(typeof(VisualElement));
                }
            }
            
            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var modal = (Modal) ve;
                modal._title.text = _title.GetValueFromBag(bag, cc);
            }
        }
        
        private readonly Label _title;
        private readonly Button _closeButton;
        private readonly VisualElement _container;
        private StyleLength _parentHeight;
        
        public override VisualElement contentContainer => _container;

        [PublicAPI]
        public EventHandler OnClose;
        [PublicAPI]
        public bool IsOpen { get; private set; }

        private VisualElement _anchor;
        private VisualElement _originalParent;

        private bool _isTemporary;

        public Modal()
        {
            Resources.Load<VisualTreeAsset>("Modal").CloneTree(this);
            styleSheets.Add(Resources.Load<StyleSheet>("ModalStyles"));
            
            AddToClassList("d-none");
            AddToClassList("absolute");
            AddToClassList("col");

            _title = this.Q<Label>("modal-title");
            _closeButton = this.Q<Button>("modal-close-btn");
            _container = this.Q("modal-content");
            var backdrop = this.Q("modal-backdrop");
            
            
            RegisterCallback<AttachToPanelEvent>(_ =>
            {
                // Only save the initial parent, ignoring the future re-parenting
                if (_originalParent != null) return;
                _originalParent = parent;
            });
            _closeButton.clicked += Close;
            backdrop.RegisterCallback<MouseDownEvent>(_ =>
            {
                Close();
            });
        }

        public Modal(VisualElement anchor) : this()
        {
            _anchor = anchor;
        }
        
        public Modal(string title, string content, VisualElement anchor) : this(anchor)
        {
            _title.text = title;
            var splitContent = content.Split('\n');
            _container.AddToClassList("p-3");
            foreach (var line in splitContent)
            {
                var label = new Label(line)
                {
                    style =
                    {
                        whiteSpace = WhiteSpace.Normal
                    }
                };
                _container.Add(label);
            }
        }
        
        /// <summary>
        /// Shorthand method for creating and showing a modal in place
        /// Calling `Close` on such a modal - immediately removes it from the hierarchy
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="anchor"></param>
        /// <returns></returns>
        [PublicAPI]
        public static Modal CreateAndShow(string title, string content, VisualElement anchor)
        {
            var modal = new Modal(title, content, anchor)
            {
                _isTemporary = true
            };
            anchor.Add(modal);
            modal.Open();
            return modal;
        }
        
        /// <summary>
        /// Sets the element to re-anchor into
        /// </summary>
        /// <param name="anchor"></param>
        [PublicAPI]
        public void SetAnchor(VisualElement anchor)
        {
            _anchor = anchor;
            RemoveFromHierarchy();
            _anchor.Add(this);
        }

        [PublicAPI]
        public void Open()
        {
            if (IsOpen) return;
            IsOpen = true;
            RemoveFromClassList("d-none");
            _parentHeight = parent.style.height;
            schedule.Execute(() =>
            {
                if (parent.contentRect.height < layout.height + 40)
                {
                    parent.style.height = layout.height + 40;
                }
            }).ExecuteLater(1);
        }

        [PublicAPI]
        public void Close()
        {
            if (!IsOpen) return;
            IsOpen = false;
            AddToClassList("d-none");
            parent.style.height = _parentHeight;
            OnClose?.Invoke(this, EventArgs.Empty);
            if (_isTemporary)
            {
                RemoveFromHierarchy();
            }
        }
        
        [PublicAPI]
        public void SetTitle(string title)
        {
            _title.text = title;
        }
    }
}