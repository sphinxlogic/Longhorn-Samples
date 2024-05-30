//This is a list of commonly used namespaces for a pane.
using System;
using System.Collections;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Media;
using System.IO;
using System.Threading;
namespace WCP
{
  public partial class Pane1 : Window
  {
    NavigationApplication myApp;
    string[] masterWordList = { "field", "glass", "house", "thing", "nothing", "entirely", "white", "quarter", "bearing", "pretty", "considering", "mischief", "childrens", "faces", "first", "held", "rubbed", "over", "wrong", "beginning", "nose", "said", "hard", "work", "lying", "quite", "still", "trying", "purr", "doubt", "feeling", "meant", "good", "black", "sitting", "corner", "armchair", "half", "asleep", "having", "romps", "wind", "rolling", "spread", "hearth", "knots", "tangles", "running", "after", "tail", "middle", "wicked", "cried", "catching", "giving", "kiss", "really", "ought", "taught", "better", "manners", "added", "looking", "reproachful", "cross", "voice", "could", "manage", "then", "taking", "kitten", "fast", "talking", "sometimes", "herself", "demurely", "knee", "watch", "progress", "winding", "gently", "touching", "glad", "help", "might", "guessed", "only", "dinah", "making", "tidy", "watching", "boys", "getting", "stick", "bonfire", "wants", "plenty", "sticks", "Kitty", "snowed", "leave", "Never", "mind", "tomorrow", "Here", "Alice", "three", "turns", "worsted", "round", "kittens", "neck", "just", "would", "look", "scramble", "which", "yards", "wound", "they", "comfortably", "doing", "very", "nearly", "opening", "window", "putting", "snow", "deserved", "little", "mischievous", "darling", "yourself", "interrupt", "went", "holding", "finger", "going", "tell", "faults", "squeaked", "twice", "while", "morning", "deny", "heard", "pretending", "speaking", "your", "fault", "keeping", "eyes", "open", "shut", "them", "tight", "happened", "make", "more", "excuses", "listen", "number", "snowdrop", "saucer", "milk", "before", "thirsty", "were" };
    Words[] workingWordList; //working word list. includes whether the word has been used
    string _masterWord;
    string _lettersUsed;
    StringBuilder _currentWord; //A stringbuilder object with the current state of the word
    int currentImageIndex = 0; //Keeps track of which index
    int guessedRight = 0;//Keeps track of how many characters you've guessed right
    int wordIndex = 0;
    int wordsUsed = 0;
    string [] images = {@"Images\Start.gif", //The game images
                        @"Images\Game1.gif", 
                        @"Images\Game2.gif", 
                        @"Images\Game3.gif", 
                        @"Images\Game4.gif", 
                        @"Images\Game5.gif", 
                        @"Images\Lose.gif",
                        @"Images\Win.gif"};

    private void OnUIReady(object sender, EventArgs e)
    {
      guessedChar.Focus(); //Reset focus to text box
      myApp = (NavigationApplication) System.Windows.Application.Current;
      InitWordList();
      _masterWord = SelectWord();
      InitCurrentWord();
      UpdateDisplay(false);
    }

    private void InitWordList()
    {
      Words temp;
      ArrayList tempList = new ArrayList();

      for (int i = 0; i < masterWordList.Length; i++)
      {
        temp.word = masterWordList[i];
        temp.fUsed = false;
        tempList.Add(temp);
      }

      workingWordList = (Words[])tempList.ToArray(typeof(Words));
    }

    private string SelectWord()
    {
      int nextIndex;
      Random r = new Random();

      if (wordsUsed == masterWordList.Length)
      {
        MessageBox.Show("No more words");
        return "";
      }

      while (true)
      {
        nextIndex = r.Next(masterWordList.Length); //masterWordList.Length
        if (workingWordList[nextIndex].fUsed == true) //already used
          continue;
        else
        {
          _masterWord = workingWordList[nextIndex].word;
          workingWordList[nextIndex].fUsed = true;
          wordsUsed++;
          break;
        }
      }

      return workingWordList[nextIndex].word;
    }

    private void InitCurrentWord()
    {
      _currentWord = new StringBuilder();
      _lettersUsed = "";
      guessedRight = 0;
      for (int i = 0; i < _masterWord.Length; i++)
      {
        _currentWord.Append('*');
      }
    }

    private void UpdateDisplay(bool isLoss)
    {
      guessedChar.Text = ""; //Reset text box for next letter
      guessedChar.Focus(); //Reset focus to text box
      if (isLoss) //you lost
      currentState.TextContent = " " + _masterWord;
      else
      currentState.TextContent = " " + _currentWord.ToString();
      lettersUsed.TextContent = " " + _lettersUsed;
    }

    private void btnGo(object sender, ClickEventArgs e)
    {
      if(guessedChar.Text == "") //Check for empty text box
      return;

      char selectedLetter = guessedChar.Text[0];
      _lettersUsed += selectedLetter;

      bool goodGuess = false;
      for (int i = 0; i < _masterWord.Length; i++)
      {
        if (_masterWord[i] == selectedLetter)
        {
          goodGuess = true;
          if (_currentWord[i] == '*')
          {
            guessedRight++;
            _currentWord[i] = _masterWord[i];
          }
        }
      }

      UpdateDisplay(false);
      if (goodGuess == true)
      {
        if (guessedRight == _masterWord.Length) //You Won.
        {
          currentImageIndex = images.Length - 1; //The win image is the last on the list
          gameImage.Source = ImageData.Create(images[currentImageIndex]);
          goButton.IsEnabled = false; //Disable Go button until new word is selected
        }
        return;
      }

      if (goodGuess == false) //Bad guess. Display next image
      {
        gameImage.Source = ImageData.Create(images[++currentImageIndex]);
        if (currentImageIndex == images.Length - 2) //You lost. Display whole word
        {
          UpdateDisplay(true);
          goButton.IsEnabled = false; //Disable Go button until new word is selected
        }
      }       
    }

    private void btnNewWord(object sender, ClickEventArgs e)
    {
      _masterWord = SelectWord();
      InitCurrentWord();
      UpdateDisplay(false);
      goButton.IsEnabled = true; //Enable Go button
      currentImageIndex = 0;
      gameImage.Source = ImageData.Create(images[currentImageIndex]);
    }

    private void btnQuit(object sender, ClickEventArgs e)
    {
      myApp.Shutdown();
    }
  }

  public struct Words
  {
    public string word;
    public bool fUsed;
  }
}