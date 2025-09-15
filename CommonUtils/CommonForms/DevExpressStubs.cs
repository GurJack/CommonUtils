#if CI_BUILD
// Заглушки для DevExpress типов при сборке в CI/CD
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DevExpress.XtraEditors
{
    public class XtraForm : Form
    {
        public XtraForm() : base() { }
    }

    public class SimpleButton : Button
    {
        public SimpleButton() : base() { }
    }

    public class LabelControl : Label
    {
        public LabelControl() : base() { }
    }

    public class TextEdit : TextBox
    {
        public class TextEditProperties
        {
            public bool UseSystemPasswordChar { get; set; }
        }

        private TextEditProperties _properties = new TextEditProperties();
        public TextEditProperties Properties => _properties;

        public TextEdit() : base() { }
    }

    public class PanelControl : Panel
    {
        public PanelControl() : base() { }
    }

    public static class XtraMessageBox
    {
        public static DialogResult Show(string text)
        {
            return MessageBox.Show(text);
        }

        public static DialogResult Show(string text, string caption)
        {
            return MessageBox.Show(text, caption);
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
        {
            return MessageBox.Show(text, caption, buttons);
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(text, caption, buttons, icon);
        }
    }
}

namespace DevExpress.XtraRichEdit
{
    public class RichEditControl : RichTextBox
    {
        public RichEditControl() : base() { }

        public API.Native.Document Document => new API.Native.Document();

        public class Options
        {
            public DocumentCapabilities DocumentCapabilities { get; set; } = new DocumentCapabilities();
        }

        public Options Options { get; set; } = new Options();
    }
}

namespace DevExpress.XtraRichEdit.API.Native
{
    public class Document
    {
        public CharacterProperties DefaultCharacterProperties { get; set; } = new CharacterProperties();
    }

    public class CharacterProperties
    {
        public string FontName { get; set; }
        public int FontSize { get; set; }
        public Color ForeColor { get; set; }
    }

    public class DocumentRange { }
}

namespace DevExpress.XtraRichEdit
{
    public enum DocumentCapability
    {
        Disabled,
        Enabled
    }

    public class DocumentCapabilities
    {
        public DocumentCapability ActiveX { get; set; }
        public DocumentCapability Bookmarks { get; set; }
        public DocumentCapability Comments { get; set; }
        public DocumentCapability Fields { get; set; }
        public DocumentCapability HeadersFooters { get; set; }
        public DocumentCapability InlineShapes { get; set; }
        public DocumentCapability Macros { get; set; }
        public DocumentCapability OleObjects { get; set; }
        public DocumentCapability ParagraphBorders { get; set; }
        public DocumentCapability ParagraphFormatting { get; set; }
        public DocumentCapability ParagraphFrames { get; set; }
        public DocumentCapability Paragraphs { get; set; }
        public DocumentCapability ParagraphStyle { get; set; }
        public DocumentCapability ParagraphTabs { get; set; }

        public NumberingCapabilities Numbering { get; set; } = new NumberingCapabilities();
    }

    public class NumberingCapabilities
    {
        public DocumentCapability Bulleted { get; set; }
        public DocumentCapability MultiLevel { get; set; }
        public DocumentCapability Simple { get; set; }
    }
}
#endif
