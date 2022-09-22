using System;
using UnityEditor;
using UnityEditor.Experimental;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Helpers
{
    public class Styles
    {
        public const int KInspectorPaddingLeft = 18;
        public const int KInspectorPaddingRight = 4;
        public const int KInspectorPaddingTop = 4;
        private GUIStyle _label;
        private GUIStyle _miniLabel;
        private GUIStyle _largeLabel;
        private GUIStyle _boldLabel;
        private GUIStyle _miniBoldLabel;
        private GUIStyle _centeredGreyMiniLabel;
        private GUIStyle _wordWrappedMiniLabel;
        private GUIStyle _wordWrappedLabel;
        private GUIStyle _linkLabel;
        private GUIStyle _whiteLabel;
        private GUIStyle _whiteMiniLabel;
        private GUIStyle _whiteLargeLabel;
        private GUIStyle _whiteBoldLabel;
        private GUIStyle _radioButton;
        private GUIStyle _miniButton;
        private GUIStyle _miniButtonLeft;
        private GUIStyle _miniButtonMid;
        private GUIStyle _miniButtonRight;
        private GUIStyle _miniPullDown;
        private GUIStyle _textField;
        private GUIStyle _boldTextField;
        private GUIStyle _textArea;
        private GUIStyle _miniTextField;
        private GUIStyle _numberField;
        private GUIStyle _popup;
        private GUIStyle _objectField;
        private GUIStyle _objectFieldButton;
        private GUIStyle _objectFieldThumb;
        private GUIStyle _objectFieldMiniThumb;
        private GUIStyle _colorField;
        private GUIStyle _layerMaskField;
        private GUIStyle _toggle;
        private GUIStyle _toggleMixed;
        private GUIStyle _foldout;
        private GUIStyle _titlebarFoldout;
        private GUIStyle _foldoutPreDrop;
        private GUIStyle _foldoutHeader;
        private GUIStyle _foldoutHeaderIcon;
        private GUIStyle _optionsButtonStyle;
        private GUIStyle _toggleGroup;
        private GUIStyle _textFieldDropDown;
        private GUIStyle _textFieldDropDownText;
        private GUIStyle _overrideMargin;
        private GUIStyle _toolbar;
        private GUIStyle _contentToolbar;
        private GUIStyle _toolbarButton;
        private GUIStyle _toolbarButtonLeft;
        private GUIStyle _toolbarButtonRight;
        private GUIStyle _toolbarPopup;
        private GUIStyle _toolbarPopupLeft;
        private GUIStyle _toolbarPopupRight;
        private GUIStyle _toolbarDropDownLeft;
        private GUIStyle _toolbarDropDown;
        private GUIStyle _toolbarDropDownRight;
        private GUIStyle _toolbarDropDownToggle;
        private GUIStyle _toolbarDropDownToggleButton;
        private GUIStyle _toolbarDropDownToggleRight;
        private GUIStyle _toolbarCreateAddNewDropDown;
        private GUIStyle _toolbarTextField;
        private GUIStyle _toolbarLabel;
        private GUIStyle _inspectorDefaultMargins;
        private GUIStyle _inspectorFullWidthMargins;
        private GUIStyle _defaultContentMargins;
        private GUIStyle _frameBox;
        private GUIStyle _helpBox;
        private GUIStyle _toolbarSearchField;
        private GUIStyle _toolbarSearchFieldPopup;
        private GUIStyle _toolbarSearchFieldWithJumpSynced;
        private GUIStyle _toolbarSearchFieldWithJumpPopupSynced;
        private GUIStyle _toolbarSearchFieldWithJump;
        private GUIStyle _toolbarSearchFieldWithJumpPopup;
        private GUIStyle _toolbarSearchFieldJumpButton;
        private GUIStyle _toolbarSearchFieldCancelButton;
        private GUIStyle _toolbarSearchFieldCancelButtonEmpty;
        private GUIStyle _toolbarSearchFieldCancelButtonWithJump;
        private GUIStyle _toolbarSearchFieldCancelButtonWithJumpEmpty;
        private GUIStyle _colorPickerBox;
        private GUIStyle _viewBg;
        private GUIStyle _inspectorBig;
        private GUIStyle _inspectorTitlebar;
        private GUIStyle _inspectorTitlebarFlat;
        private GUIStyle _inspectorTitlebarText;
        private GUIStyle _foldoutSelected;
        private GUIStyle _iconButton;
        private GUIStyle _tooltip;
        private GUIStyle _notificationText;
        private GUIStyle _notificationBackground;
        private GUIStyle _assetLabel;
        private GUIStyle _assetLabelPartial;
        private GUIStyle _assetLabelIcon;
        private GUIStyle _searchField;
        private GUIStyle _searchFieldCancelButton;
        private GUIStyle _searchFieldCancelButtonEmpty;
        private GUIStyle _selectionRect;
        private GUIStyle _toolbarSlider;
        private GUIStyle _minMaxHorizontalSliderThumb;
        private GUIStyle _dropDownList;
        private GUIStyle _dropDownToggleButton;
        private GUIStyle _minMaxStateDropdown;
        private GUIStyle _progressBarBar;
        private GUIStyle _progressBarText;
        private GUIStyle _progressBarBack;
        private GUIStyle _scrollViewAlt;
        private Vector2 _knobSize = new Vector2(40f, 40f);
        private Vector2 _miniKnobSize = new Vector2(29f, 29f);

        private static Styles _current;
        private GUIStyle _defaultContentPaddings;

        private static Styles Current
        {
            get
            {
                if (_current != null) return _current;
                _current = new Styles();
                _current.InitSharedStyles();

                return _current;
            }
        }

        /// <summary>
        ///   <para>Style used for the labelled on all EditorGUI overloads that take a prefix label.</para>
        /// </summary>
        public static GUIStyle Label => Current._label;

        /// <summary>
        ///   <para>Style for label with small font.</para>
        /// </summary>
        public static GUIStyle MiniLabel => Current._miniLabel;

        /// <summary>
        ///   <para>Style for label with large font.</para>
        /// </summary>
        public static GUIStyle LargeLabel => Current._largeLabel;

        /// <summary>
        ///   <para>Style for bold label.</para>
        /// </summary>
        public static GUIStyle BoldLabel => Current._boldLabel;

        /// <summary>
        ///   <para>Style for mini bold label.</para>
        /// </summary>
        public static GUIStyle MiniBoldLabel => Current._miniBoldLabel;

        /// <summary>
        ///   <para>Style for label with small font which is centered and grey.</para>
        /// </summary>
        public static GUIStyle CenteredGreyMiniLabel => Current._centeredGreyMiniLabel;

        /// <summary>
        ///   <para>Style for word wrapped mini label.</para>
        /// </summary>
        public static GUIStyle WordWrappedMiniLabel => Current._wordWrappedMiniLabel;

        /// <summary>
        ///   <para>Style for word wrapped label.</para>
        /// </summary>
        public static GUIStyle WordWrappedLabel => Current._wordWrappedLabel;

        /// <summary>
        ///   <para>Style used for links.</para>
        /// </summary>
        public static GUIStyle LinkLabel => Current._linkLabel;

        /// <summary>
        ///   <para>Style for white label.</para>
        /// </summary>
        public static GUIStyle WhiteLabel => Current._whiteLabel;

        /// <summary>
        ///   <para>Style for white mini label.</para>
        /// </summary>
        public static GUIStyle WhiteMiniLabel => Current._whiteMiniLabel;

        /// <summary>
        ///   <para>Style for white large label.</para>
        /// </summary>
        public static GUIStyle WhiteLargeLabel => Current._whiteLargeLabel;

        /// <summary>
        ///   <para>Style for white bold label.</para>
        /// </summary>
        public static GUIStyle WhiteBoldLabel => Current._whiteBoldLabel;

        /// <summary>
        ///   <para>Style used for a radio button.</para>
        /// </summary>
        public static GUIStyle RadioButton => Current._radioButton;

        /// <summary>
        ///   <para>Style used for a standalone small button.</para>
        /// </summary>
        public static GUIStyle MiniButton => Current._miniButton;

        /// <summary>
        ///   <para>Style used for the leftmost button in a horizontal button group.</para>
        /// </summary>
        public static GUIStyle MiniButtonLeft => Current._miniButtonLeft;

        /// <summary>
        ///   <para>Style used for the middle buttons in a horizontal group.</para>
        /// </summary>
        public static GUIStyle MiniButtonMid => Current._miniButtonMid;

        /// <summary>
        ///   <para>Style used for the rightmost button in a horizontal group.</para>
        /// </summary>
        public static GUIStyle MiniButtonRight => Current._miniButtonRight;

        /// <summary>
        ///   <para>Style used for the drop-down controls.</para>
        /// </summary>
        public static GUIStyle MiniPullDown => Current._miniPullDown;

        /// <summary>
        ///   <para>Style used for EditorGUI.TextField.</para>
        /// </summary>
        public static GUIStyle TextField => Current._textField;

        public static GUIStyle BoldTextField => Current._boldTextField;

        /// <summary>
        ///   <para>Style used for EditorGUI.TextArea.</para>
        /// </summary>
        public static GUIStyle TextArea => Current._textArea;

        /// <summary>
        ///   <para>Smaller text field.</para>
        /// </summary>
        public static GUIStyle MiniTextField => Current._miniTextField;

        /// <summary>
        ///   <para>Style used for field editors for numbers.</para>
        /// </summary>
        public static GUIStyle NumberField => Current._numberField;

        /// <summary>
        ///   <para>Style used for EditorGUI.Popup, EditorGUI.EnumPopup,.</para>
        /// </summary>
        public static GUIStyle Popup => Current._popup;

        /// <summary>
        ///   <para>Style used for headings for object fields.</para>
        /// </summary>
        public static GUIStyle ObjectField => Current._objectField;

        public static GUIStyle ObjectFieldButton => Current._objectFieldButton;

        /// <summary>
        ///   <para>Style used for headings for the Select button in object fields.</para>
        /// </summary>
        public static GUIStyle ObjectFieldThumb => Current._objectFieldThumb;

        /// <summary>
        ///   <para>Style used for object fields that have a thumbnail (e.g Textures). </para>
        /// </summary>
        public static GUIStyle ObjectFieldMiniThumb => Current._objectFieldMiniThumb;

        /// <summary>
        ///   <para>Style used for headings for Color fields.</para>
        /// </summary>
        public static GUIStyle ColorField => Current._colorField;

        /// <summary>
        ///   <para>Style used for headings for Layer masks.</para>
        /// </summary>
        public static GUIStyle LayerMaskField => Current._layerMaskField;

        /// <summary>
        ///   <para>Style used for headings for EditorGUI.Toggle.</para>
        /// </summary>
        public static GUIStyle Toggle => Current._toggle;

        public static GUIStyle ToggleMixed => Current._toggleMixed;

        /// <summary>
        ///   <para>Style used for headings for EditorGUI.Foldout.</para>
        /// </summary>
        public static GUIStyle Foldout => Current._foldout;

        public static GUIStyle TitlebarFoldout => Current._titlebarFoldout;

        /// <summary>
        ///   <para>Style used for headings for EditorGUI.Foldout.</para>
        /// </summary>
        public static GUIStyle FoldoutPreDrop => Current._foldoutPreDrop;

        /// <summary>
        ///   <para>Style used for headings for EditorGUILayout.BeginFoldoutHeaderGroup.</para>
        /// </summary>
        public static GUIStyle FoldoutHeader => Current._foldoutHeader;

        /// <summary>
        ///   <para>Style used for icon for EditorGUILayout.BeginFoldoutHeaderGroup.</para>
        /// </summary>
        public static GUIStyle FoldoutHeaderIcon => Current._foldoutHeaderIcon;

        public static GUIStyle OptionsButtonStyle => Current._optionsButtonStyle;

        /// <summary>
        ///   <para>Style used for headings for EditorGUILayout.BeginToggleGroup.</para>
        /// </summary>
        public static GUIStyle ToggleGroup => Current._toggleGroup;

        public static GUIStyle TextFieldDropDown => Current._textFieldDropDown;

        public static GUIStyle TextFieldDropDownText => Current._textFieldDropDownText;

        public static GUIStyle OverrideMargin => Current._overrideMargin;

        /// <summary>
        ///   <para>Toolbar background from top of windows.</para>
        /// </summary>
        public static GUIStyle Toolbar => Current._toolbar;

        public static GUIStyle ContentToolbar => Current._contentToolbar;

        /// <summary>
        ///   <para>Style for Button and Toggles in toolbars.</para>
        /// </summary>
        public static GUIStyle ToolbarButton => Current._toolbarButton;

        public static GUIStyle ToolbarButtonLeft => Current._toolbarButtonLeft;

        public static GUIStyle ToolbarButtonRight => Current._toolbarButtonRight;

        /// <summary>
        ///   <para>Toolbar Popup.</para>
        /// </summary>
        public static GUIStyle ToolbarPopup => Current._toolbarPopup;

        public static GUIStyle ToolbarPopupLeft => Current._toolbarPopupLeft;

        public static GUIStyle ToolbarPopupRight => Current._toolbarPopupRight;

        public static GUIStyle ToolbarDropDownLeft => Current._toolbarDropDownLeft;

        /// <summary>
        ///   <para>Toolbar Dropdown.</para>
        /// </summary>
        public static GUIStyle ToolbarDropDown => Current._toolbarDropDown;

        public static GUIStyle ToolbarDropDownRight => Current._toolbarDropDownRight;

        public static GUIStyle ToolbarDropDownToggle => Current._toolbarDropDownToggle;

        public static GUIStyle ToolbarDropDownToggleButton => Current._toolbarDropDownToggleButton;

        public static GUIStyle ToolbarDropDownToggleRight => Current._toolbarDropDownToggleRight;

        public static GUIStyle ToolbarCreateAddNewDropDown => Current._toolbarCreateAddNewDropDown;

        /// <summary>
        ///   <para>Toolbar text field.</para>
        /// </summary>
        public static GUIStyle ToolbarTextField => Current._toolbarTextField;

        public static GUIStyle ToolbarLabel => Current._toolbarLabel;

        /// <summary>
        ///   <para>Wrap content in a vertical group with this style to get the default margins used in the Inspector.</para>
        /// </summary>
        public static GUIStyle InspectorDefaultMargins => Current._inspectorDefaultMargins;

        /// <summary>
        ///   <para>Wrap content in a vertical group with this style to get full width margins in the Inspector.</para>
        /// </summary>
        public static GUIStyle InspectorFullWidthMargins => Current._inspectorFullWidthMargins;

        public static GUIStyle DefaultContentMargins => Current._defaultContentMargins;
        public static GUIStyle DefaultContentPaddings => Current._defaultContentPaddings;

        public static GUIStyle FrameBox => Current._frameBox;

        /// <summary>
        ///   <para>Style used for background box for EditorGUI.HelpBox.</para>
        /// </summary>
        public static GUIStyle HelpBox => Current._helpBox;

        /// <summary>
        ///   <para>Toolbar search field.</para>
        /// </summary>
        public static GUIStyle ToolbarSearchField => Current._toolbarSearchField;

        public static GUIStyle ToolbarSearchFieldPopup => Current._toolbarSearchFieldPopup;

        public static GUIStyle ToolbarSearchFieldWithJumpSynced => Current._toolbarSearchFieldWithJumpSynced;

        public static GUIStyle ToolbarSearchFieldWithJumpPopupSynced =>
            Current._toolbarSearchFieldWithJumpPopupSynced;

        public static GUIStyle ToolbarSearchFieldWithJump => Current._toolbarSearchFieldWithJump;

        public static GUIStyle ToolbarSearchFieldWithJumpPopup => Current._toolbarSearchFieldWithJumpPopup;

        public static GUIStyle ToolbarSearchFieldJumpButton => Current._toolbarSearchFieldJumpButton;

        public static GUIStyle ToolbarSearchFieldCancelButton => Current._toolbarSearchFieldCancelButton;

        public static GUIStyle ToolbarSearchFieldCancelButtonEmpty => Current._toolbarSearchFieldCancelButtonEmpty;

        public static GUIStyle ToolbarSearchFieldCancelButtonWithJump =>
            Current._toolbarSearchFieldCancelButtonWithJump;

        public static GUIStyle ToolbarSearchFieldCancelButtonWithJumpEmpty =>
            Current._toolbarSearchFieldCancelButtonWithJumpEmpty;

        public static GUIStyle ColorPickerBox => Current._colorPickerBox;

        public static GUIStyle ViewBackground => Current._viewBg;

        public static GUIStyle InspectorBig => Current._inspectorBig;

        public static GUIStyle InspectorTitlebar => Current._inspectorTitlebar;

        public static GUIStyle InspectorTitlebarFlat => Current._inspectorTitlebarFlat;

        public static GUIStyle InspectorTitlebarText => Current._inspectorTitlebarText;

        public static GUIStyle FoldoutSelected => Current._foldoutSelected;

        /// <summary>
        ///   <para>Style used for a standalone icon button.</para>
        /// </summary>
        public static GUIStyle IconButton => Current._iconButton;

        public static GUIStyle Tooltip => Current._tooltip;

        public static GUIStyle NotificationText => Current._notificationText;

        public static GUIStyle NotificationBackground => Current._notificationBackground;

        public static GUIStyle AssetLabel => Current._assetLabel;

        public static GUIStyle AssetLabelPartial => Current._assetLabelPartial;

        public static GUIStyle AssetLabelIcon => Current._assetLabelIcon;

        public static GUIStyle SearchField => Current._searchField;

        public static GUIStyle SearchFieldCancelButton => Current._searchFieldCancelButton;

        public static GUIStyle SearchFieldCancelButtonEmpty => Current._searchFieldCancelButtonEmpty;

        /// <summary>
        ///   <para>Style used to draw a marquee selection rect in the SceneView.</para>
        /// </summary>
        public static GUIStyle SelectionRect => Current._selectionRect;

        public static GUIStyle ToolbarSlider => Current._toolbarSlider;

        public static GUIStyle MinMaxHorizontalSliderThumb => Current._minMaxHorizontalSliderThumb;

        public static GUIStyle DropDownList => Current._dropDownList;

        public static GUIStyle DropDownToggleButton => Current._dropDownToggleButton;

        public static GUIStyle MinMaxStateDropdown => Current._minMaxStateDropdown;

        public static GUIStyle ProgressBarBack => Current._progressBarBack;

        public static GUIStyle ProgressBarBar => Current._progressBarBar;

        public static GUIStyle ProgressBarText => Current._progressBarText;

        public static GUIStyle ScrollViewAlt => Current._scrollViewAlt;

        public static Vector2 KnobSize => Current._knobSize;

        public static Vector2 MiniKnobSize => Current._miniKnobSize;

        private void InitSharedStyles()
        {
            _colorPickerBox = GetStyle("ColorPickerBox");
            _viewBg = GetStyle("TabWindowBackground");
            _inspectorBig = GetStyle("In BigTitle");
            _miniLabel = GetStyle("MiniLabel");
            _largeLabel = GetStyle("LargeLabel");
            _boldLabel = GetStyle("BoldLabel");
            _miniBoldLabel = GetStyle("MiniBoldLabel");
            _wordWrappedLabel = GetStyle("WordWrappedLabel");
            _wordWrappedMiniLabel = GetStyle("WordWrappedMiniLabel");
            _whiteLabel = GetStyle("WhiteLabel");
            _whiteMiniLabel = GetStyle("WhiteMiniLabel");
            _whiteLargeLabel = GetStyle("WhiteLargeLabel");
            _whiteBoldLabel = GetStyle("WhiteBoldLabel");
            _miniTextField = GetStyle("MiniTextField");
            _radioButton = GetStyle("Radio");
            _miniButton = GetStyle("miniButton");
            _miniButtonLeft = GetStyle("miniButtonLeft");
            _miniButtonMid = GetStyle("miniButtonMid");
            _miniButtonRight = GetStyle("miniButtonRight");
            _miniPullDown = GetStyle("MiniPullDown");
            _toolbar = GetStyle("toolbar");
            _contentToolbar = GetStyle("contentToolbar");
            _toolbarButton = GetStyle("toolbarbutton");
            _toolbarButtonLeft = GetStyle("toolbarbuttonLeft");
            _toolbarButtonRight = GetStyle("toolbarbuttonRight");
            _toolbarPopup = GetStyle("toolbarPopup");
            _toolbarPopupLeft = GetStyle("toolbarPopupLeft");
            _toolbarPopupRight = GetStyle("toolbarPopupRight");
            _toolbarDropDown = GetStyle("toolbarDropDown");
            _toolbarDropDownLeft = GetStyle("toolbarDropDownLeft");
            _toolbarDropDownRight = GetStyle("toolbarDropDownRight");
            _toolbarDropDownToggle = GetStyle("toolbarDropDownToggle");
            _toolbarDropDownToggleButton = GetStyle("ToolbarDropDownToggleButton");
            _toolbarDropDownToggleRight = GetStyle("toolbarDropDownToggleRight");
            _toolbarCreateAddNewDropDown = GetStyle("ToolbarCreateAddNewDropDown");
            _toolbarTextField = GetStyle("toolbarTextField");
            _toolbarLabel = GetStyle("ToolbarLabel");
            _toolbarSearchField = GetStyle("ToolbarSeachTextField");
            _toolbarSearchFieldPopup = GetStyle("ToolbarSeachTextFieldPopup");
            _toolbarSearchFieldWithJump = GetStyle("ToolbarSearchTextFieldWithJump");
            _toolbarSearchFieldWithJumpPopup = GetStyle("ToolbarSearchTextFieldWithJumpPopup");
            _toolbarSearchFieldJumpButton = GetStyle("ToolbarSearchTextFieldJumpButton");
            _toolbarSearchFieldCancelButton = GetStyle("ToolbarSeachCancelButton");
            _toolbarSearchFieldCancelButtonEmpty = GetStyle("ToolbarSeachCancelButtonEmpty");
            _toolbarSearchFieldCancelButtonWithJump = GetStyle("ToolbarSearchCancelButtonWithJump");
            _toolbarSearchFieldCancelButtonWithJumpEmpty = GetStyle("ToolbarSearchCancelButtonWithJumpEmpty");
            _toolbarSearchFieldWithJumpSynced = GetStyle("ToolbarSearchTextFieldWithJumpSynced");
            _toolbarSearchFieldWithJumpPopupSynced = GetStyle("ToolbarSearchTextFieldWithJumpPopupSynced");
            _searchField = GetStyle("SearchTextField");
            _searchFieldCancelButton = GetStyle("SearchCancelButton");
            _searchFieldCancelButtonEmpty = GetStyle("SearchCancelButtonEmpty");
            _helpBox = GetStyle("HelpBox");
            _frameBox = GetStyle("FrameBox");
            _assetLabel = GetStyle("AssetLabel");
            _assetLabelPartial = GetStyle("AssetLabel Partial");
            _assetLabelIcon = GetStyle("AssetLabel Icon");
            _selectionRect = GetStyle("selectionRect");
            _toolbarSlider = GetStyle("ToolbarSlider");
            _minMaxHorizontalSliderThumb = GetStyle("MinMaxHorizontalSliderThumb");
            _dropDownList = GetStyle("DropDownButton");
            _dropDownToggleButton = GetStyle("DropDownToggleButton");
            _minMaxStateDropdown = GetStyle("IN MinMaxStateDropdown");
            _progressBarBack = GetStyle("ProgressBarBack");
            _progressBarBar = GetStyle("ProgressBarBar");
            _progressBarText = GetStyle("ProgressBarText");
            _foldoutPreDrop = GetStyle("FoldoutPreDrop");
            _foldoutHeader = GetStyle("FoldoutHeader");
            _foldoutHeaderIcon = GetStyle("FoldoutHeaderIcon");
            _optionsButtonStyle = GetStyle("PaneOptions");
            _inspectorTitlebar = GetStyle("IN Title");
            _inspectorTitlebarFlat = GetStyle("IN Title Flat");
            _inspectorTitlebarText = GetStyle("IN TitleText");
            _toggleGroup = GetStyle("BoldToggle");
            _tooltip = GetStyle("Tooltip");
            _notificationText = GetStyle("NotificationText");
            _notificationBackground = GetStyle("NotificationBackground");
            _scrollViewAlt = GetStyle("ScrollViewAlt");
            _popup = _layerMaskField = GetStyle("MiniPopup");
            _textField = _numberField = GetStyle("TextField");
            _boldTextField = GetStyle("BoldTextFIeld");
            _label = GetStyle("ControlLabel");
            _objectField = GetStyle("ObjectField");
            _objectFieldThumb = GetStyle("ObjectFieldThumb");
            _objectFieldButton = GetStyle("ObjectFieldButton");
            _objectFieldMiniThumb = GetStyle("ObjectFieldMiniThumb");
            _toggle = GetStyle("Toggle");
            _toggleMixed = GetStyle("ToggleMixed");
            _colorField = GetStyle("ColorField");
            _foldout = GetStyle("Foldout");
            _titlebarFoldout = GetStyle("Titlebar Foldout");
            _foldoutSelected = GUIStyle.none;
            _iconButton = GetStyle("IconButton");
            _textFieldDropDown = GetStyle("TextFieldDropDown");
            _textFieldDropDownText = GetStyle("TextFieldDropDownText");
            _overrideMargin = GetStyle("OverrideMargin");
            _linkLabel = GetStyle("LinkLabel");
            _textArea = new GUIStyle(_textField)
            {
                wordWrap = true
            };
            _inspectorDefaultMargins = new GUIStyle
            {
                padding = new RectOffset(18, 4, 4, 0)
            };
            _inspectorFullWidthMargins = new GUIStyle
            {
                padding = new RectOffset(5, 4, 0, 0)
            };
            _defaultContentMargins = new GUIStyle
            {
                padding = new RectOffset(4, 4, 4, 4)
            };
            _centeredGreyMiniLabel = new GUIStyle(_miniLabel)
            {
                alignment = TextAnchor.MiddleCenter,
                normal =
                {
                    textColor = Color.grey
                },
                hover =
                {
                    textColor = Color.grey
                },
                active =
                {
                    textColor = Color.grey
                },
                focused =
                {
                    textColor = Color.grey
                }
            };
        }

        public GUIStyle GetStyle(string styleName)
        {
            GUIStyle style = GUI.skin.FindStyle(styleName) ??
                             EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector).FindStyle(styleName);
            if (style == null)
            {
                Debug.LogError("Missing built-in guistyle " + styleName);
                style = GUIStyle.none;
            }

            return style;
        }
    }
}