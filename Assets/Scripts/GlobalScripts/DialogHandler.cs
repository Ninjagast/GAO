using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace GlobalScripts
{
    public class DialogHandler: MonoBehaviour
    {
        
        private static DialogHandler _current;
        public static DialogHandler Current => _current;

        public GameObject DialogBox;
        public TextMeshProUGUI DialogText;
        public List<GameObject> heads;
        
//      id 0 is player
//      id 1 is the boss
        private readonly List<List<KeyValuePair<int, String>>> _dialogList = new();
        private List<KeyValuePair<int, String>> _currentDialog = new();
        private Random _random;
        private int _currentTextIndex = 0;
        private int _waited = 0;

        private bool _waitingForNextInput = true;
        private bool _skipText = false;
        
        private void Awake()
        {
            _current = this;
            _random = new Random();
        }

        private void Start()
        {
//          from is based on the loading state of the game
            LoadDialogList(-1);
            Application.targetFrameRate = 60;
        }

        private void Update()
        {
            if (_waited < 40)
            {
                _waited++;
            }
//          todo waiting times based on options
            if (_currentDialog.Count > 0 && !_waitingForNextInput && _waited > 2)
            {
                _waited = 0;

                if (_skipText)
                {
                    DialogText.text = _currentDialog[0].Value;
                    _waitingForNextInput = true;
                    _currentTextIndex = 0;
                    _skipText = false;
                }
                else
                {
                    if (_currentTextIndex + 1 > _currentDialog[0].Value.Length)
                    {
                        DialogText.text = _currentDialog[0].Value;
                        _waitingForNextInput = true;
                        _currentTextIndex = 0;
                    }
                    else
                    {
                        DialogText.text = _currentDialog[0].Value
                            .Substring(0, _currentTextIndex);
                        _currentTextIndex++;
                    }   
                }
            }
        }

        private void LoadDialogList(int from)
        {
            _dialogList.Add(new List<KeyValuePair<int, string>>());
            string currentDialogId = "0";
            
//          example line. DialogID;Character;Text
//          3;1;example
            using (var reader = new StreamReader(Application.dataPath + "/Dialog.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line != null)
                    {
                        var values = line.Split(";");
                        if (Int32.Parse(values[0]) > from)
                        {
                            if (values[0] == currentDialogId)
                            {
                                _dialogList[^1].Add(new KeyValuePair<int, string>(Int32.Parse(values[1]), values[2]));
                            }
                            else
                            {
                                _dialogList.Add(new List<KeyValuePair<int, string>>());
                                _dialogList[^1].Add(new KeyValuePair<int, string>(Int32.Parse(values[1]), values[2]));
                                currentDialogId = values[0];
                            }
                        }
                    }
                }
            }
        }

        public void TriggerNextInput()
        {
            if (_waitingForNextInput)
            {
                _waitingForNextInput = false;
                if (_currentDialog.Count < 2)
                {
                    DialogBox.SetActive(false);
                    _currentDialog = new List<KeyValuePair<int, string>>();
                }
                else
                {
                    heads[_currentDialog[0].Key].SetActive(false);
                    _currentDialog.RemoveAt(0);
                    heads[_currentDialog[0].Key].SetActive(true);
                }
            }
            else
            {
                _skipText = true;
            }
        }
        
        public void GetNextDialog()
        {
            if (_currentDialog.Count < 1)
            {
                _currentDialog = _dialogList[0];
                _dialogList.RemoveAt(0);
                DialogBox.SetActive(true);
                heads[_currentDialog[0].Key].SetActive(true);
                _waitingForNextInput = false;
                
            }
        }
    }
}