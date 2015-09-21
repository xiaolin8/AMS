using System;
using System.Collections.Generic;

namespace DotNet.Utilities
{
    ///<summary>
    ///TreeView
    ///TreeView树
    ///<summary>	
    [Serializable()]
    public class TreeView
    {
        private bool _showcheck = true;
        private bool _isexpand = true;
        private bool _complete = true;
        private bool _hasChildren = false;
        private string _parentnodes = "0";
        private string _checkstate = "0";
       

        public string checkstate
        {
            get { return _checkstate; }
            set { _checkstate = value; }
        }
        public string id { get; set; }
        public string text { get; set; }
        public string value { get; set; }
        public string Location { get; set; }
        public string img { get; set; }
        public string parentnodes
        {
            get
            {
                return this._parentnodes;
            }
            set
            {
                this._parentnodes = value;
            }
        }
        public bool showcheck
        {
            get
            {
                return this._showcheck;
            }
            set
            {
                this._showcheck = value;
            }
        }
        public bool isexpand
        {
            get
            {
                return this._isexpand;
            }
            set
            {
                this._isexpand = value;
            }
        }
        public bool complete
        {
            get
            {
                return this._complete;
            }
            set
            {
                this._complete = value;
            }
        }
        public bool hasChildren
        {
            get
            {
                return this._hasChildren;
            }
            set
            {
                this._hasChildren = value;
            }
        }
        public List<TreeView> ChildNodes { get; set; }
    }

}
