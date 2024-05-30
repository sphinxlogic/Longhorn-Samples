Imports System
Imports System.Text
Imports System.Collections
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Data
Imports System.Windows.Media


Namespace WordGame1_vb
  Partial Class Pane1
    Dim myApp As NavigationApplication
    Dim masterWordList As String() = {"field", "glass", "house", "thing", "nothing", "entirely", "white", "quarter", "bearing", "pretty", "considering", "mischief", "childrens", "faces", "first", "held", "rubbed", "over", "wrong", "beginning", "nose", "said", "hard", "work", "lying", "quite", "still", "trying", "purr", "doubt", "feeling", "meant", "good", "black", "sitting", "corner", "armchair", "half", "asleep", "having", "romps", "wind", "rolling", "spread", "hearth", "knots", "tangles", "running", "after", "tail", "middle", "wicked", "cried", "catching", "giving", "kiss", "really", "ought", "taught", "better", "manners", "added", "looking", "reproachful", "cross", "voice", "could", "manage", "then", "taking", "kitten", "fast", "talking", "sometimes", "herself", "demurely", "knee", "watch", "progress", "winding", "gently", "touching", "glad", "help", "might", "guessed", "only", "dinah", "making", "tidy", "watching", "boys", "getting", "stick", "bonfire", "wants", "plenty", "sticks", "Kitty", "snowed", "leave", "Never", "mind", "tomorrow", "Here", "Alice", "three", "turns", "worsted", "round", "kittens", "neck", "just", "would", "look", "scramble", "which", "yards", "wound", "they", "comfortably", "doing", "very", "nearly", "opening", "window", "putting", "snow", "deserved", "little", "mischievous", "darling", "yourself", "interrupt", "went", "holding", "finger", "going", "tell", "faults", "squeaked", "twice", "while", "morning", "deny", "heard", "pretending", "speaking", "your", "fault", "keeping", "eyes", "open", "shut", "them", "tight", "happened", "make", "more", "excuses", "listen", "number", "snowdrop", "saucer", "milk", "before", "thirsty", "were"}
    Dim _masterWord As String
    Dim workingWordList As Words() 'Working word list. includes whether the word has been used
    Dim _lettersUsed As String
    Dim _currentWord As New StringBuilder()  'A stringbuilder object with the current state of the word
    Dim currentImageIndex As Integer = 0 'Keeps track of which index
    Dim guessedRight As Integer = 0 'Keeps track of how many characters you've guessed right
    Dim wordIndex As Integer = 0
    Dim wordsUsed As Integer = 0
    Dim game_Images As String() = {"Images\Start.gif", _
                                 "Images\Game1.gif", _
                                 "Images\Game2.gif", _
                                 "Images\Game3.gif", _
                                 "Images\Game4.gif", _
                                 "Images\Game5.gif", _
                                 "Images\Lose.gif", _
                                 "Images\Win.gif"}
    Structure Words
      Dim word As String
      Dim fUsed As Boolean
    End Structure

    Sub UI_Ready(ByVal Sender As Object, ByVal e As EventArgs)
      guessedChar.Focus() 'Reset focus to text box
      myApp = CType(System.Windows.Application.Current, NavigationApplication)
      InitWordList()
      _masterWord = SelectWord()
      InitCurrentWord()
      UpdateDisplay(False)
    End Sub

    Sub InitWordList()
      Dim temp As Words
      Dim tempList As New ArrayList()
      Dim i As Integer

      For i = 0 To (masterWordList.Length - 1)
        temp.word = masterWordList(i)
        temp.fUsed = False
        tempList.Add(temp)
      Next i
      workingWordList = CType(tempList.ToArray(GetType(Words)), Words())
    End Sub

    Function SelectWord() As String
      Dim nextIndex As Integer
      Dim r As New Random()
      Dim test As Boolean = True

      If wordsUsed = masterWordList.Length Then
        MessageBox.Show("No more words")
        Return ""
      End If

      Do While test
        nextIndex = r.Next(masterWordList.Length) 'masterWordList.Length
        If workingWordList(nextIndex).fUsed = True Then 'already used
          Continue Do
        Else
          _masterWord = workingWordList(nextIndex).word
          workingWordList(nextIndex).fUsed = True
          wordsUsed += 1
          test = False
        End If
      Loop
      Return workingWordList(nextIndex).word
    End Function

    Sub InitCurrentWord()
      Dim i As Integer

      _lettersUsed = ""
      guessedRight = 0
      _currentWord.Remove(0, _currentWord.Length)
      For i = 0 To (_masterWord.Length - 1)
        _currentWord.Append("*")
      Next i
    End Sub

    Sub UpdateDisplay(ByVal isLoss As Boolean)
      guessedChar.Text = "" 'Reset text box for next letter
      guessedChar.Focus() 'Reset focus to text box
      If isLoss = True Then 'you lost
        currentState.TextContent = " " & _masterWord
      Else
        currentState.TextContent = " " & _currentWord.ToString()
        lettersUsed.TextContent = " " & _lettersUsed
      End If
    End Sub

    Sub btnGo(ByVal Sender As Object, ByVal e As ClickEventArgs)
      Dim selectedLetter As Char
      Dim goodGuess As Boolean
      Dim i As Integer

      If guessedChar.Text = "" Then 'Check for empty text box
        Return
      End If

      selectedLetter = guessedChar.Text(0)
      _lettersUsed = _lettersUsed & selectedLetter

      goodGuess = False
      For i = 0 To _masterWord.Length - 1
        If _masterWord(i) = selectedLetter Then
          goodGuess = True
          If _currentWord(i) = "*" Then
            guessedRight += 1
            _currentWord(i) = _masterWord(i)
          End If
        End If
      Next i
      UpdateDisplay(False)

      If goodGuess = True Then 'Good Guess
        If guessedRight = _masterWord.Length Then 'You Won.
          currentImageIndex = game_Images.Length - 1 'The win image is the last on the list
          gameImage.Source = ImageData.Create(game_Images(currentImageIndex))
          goButton.IsEnabled = False 'Disable Go button until new word is selected
        End If
        Return
      End If

      If goodGuess = False Then 'Bad guess. Display next image
        currentImageIndex += 1
        gameImage.Source = ImageData.Create(game_Images(currentImageIndex))
        If currentImageIndex = game_Images.Length - 2 Then 'You lost. Display whole word
          UpdateDisplay(True)
          goButton.IsEnabled = False 'Disable Go button until new word is selected
        End If
      End If
    End Sub

    Sub btnNewWord(ByVal Sender As Object, ByVal e As ClickEventArgs)
      _masterWord = SelectWord()
      InitCurrentWord()
      UpdateDisplay(False)
      goButton.IsEnabled = True 'Enable Go button
      currentImageIndex = 0
      gameImage.Source = ImageData.Create(game_Images(currentImageIndex))
    End Sub

    Sub btnQuit(ByVal Sender As Object, ByVal e As ClickEventArgs)
      myApp.Shutdown()
    End Sub
  End Class
End Namespace
