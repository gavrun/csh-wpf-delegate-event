using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace csh_wpf_delegate_event;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>

// Form class acting as Publisher class
public partial class MainWindow : Window
{
    // Event for closing the form
    public event EventHandler<CloseFormEventArgs>? CloseFormEvent;

    public MainWindow()
    {
        InitializeComponent(); // ignore squiggling, caching issue

        // Create a subscriber
        var closer = new FormCloser(this);
    }


    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        // Trigger the event
        OnCloseFormEvent(new CloseFormEventArgs("Form is closing..."));
    }

    protected virtual void OnCloseFormEvent(CloseFormEventArgs e)
    {
        // Call the event if there are subscribers
        CloseFormEvent?.Invoke(this, e); 
    }
}

// Custom event args for closing form
public class CloseFormEventArgs : EventArgs
{
    public string Message { get; }

    public CloseFormEventArgs(string message)
    {
        Message = message;
    }
}

// Subscriber class
public class FormCloser
{
    private MainWindow form;

    public FormCloser(MainWindow form)
    {
        this.form = form;

        // Subscribe to the event
        form.CloseFormEvent += HandleCloseFormEvent;
    }

    private void HandleCloseFormEvent(object? sender, CloseFormEventArgs e)
    {
        MessageBox.Show(e.Message); // Show the message

        form.Close(); // Close the form
    }
}