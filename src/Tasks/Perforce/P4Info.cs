// NAnt - A .NET build tool
// Copyright (C) 2001-2002 Gerry Shaw
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
// Ian MacLean ( ian_maclean@another.com )
// Jeff Hemry ( jdhemry@qwest.net )

using System;
using System.Text;
using NAnt.Core;
using NAnt.Core.Util;
using NAnt.Core.Tasks;
using NAnt.Core.Attributes;

namespace NAnt.Contrib.Tasks.Perforce {
    /// <summary>Returns information from the "p4 info" command back into
    /// variables for use within the build process
    /// <example>
    /// <para>Fill the variables using the task.</para>
    /// <code>
    ///  <![CDATA[
    ///  <p4info user="myuser" client="myclient" host="myhost" root="myroot" />
    ///  <echo message="User: ${myuser} - Client: ${myclient} - Host: ${myhost} - Root: ${myroot}" />
    ///  ]]>
    /// </code>
    /// </example>
    /// </summary>
    [TaskName("p4info")]
        public class P4Info : P4Base {

        #region Private Instance Fields

        private string _user = null;
        private string _client = null;
        private string _host = null;
        private string _root = null;

        #endregion

        #region Public Instance Fields

        [TaskAttribute("user", Required = false)]
        [StringValidator(AllowEmpty = false)]
        public new string User {
            get { return _user; }
            set { _user = StringUtils.ConvertEmptyToNull(value); }
        }

        [TaskAttribute("client", Required = false)]
        [StringValidator(AllowEmpty = false)]
        public new string Client {
            get { return _client; }
            set { _client = StringUtils.ConvertEmptyToNull(value); }
        }

        [TaskAttribute("host", Required = false)]
        [StringValidator(AllowEmpty = false)]
        public string Host {
            get { return _host;}
            set {_host = StringUtils.ConvertEmptyToNull(value); }
        }

        [TaskAttribute("root", Required = false)]
        [StringValidator(AllowEmpty = false)]
        public string Root {
            get { return _root; }
            set { _root = StringUtils.ConvertEmptyToNull(value); }
        }

        #endregion

        protected override string CommandSpecificArguments {
            get { return ""; }
        }
        #region Override implementation of Task

        protected override void ExecuteTask() {
            string[] find = {"User name:", "Client name:", "Client host:", "Client root:"};
            string[] results = Perforce.GetP4Info(find);
            
            Project.Properties[User] = results[0].ToString();
            Project.Properties[Client] = results[1].ToString();
            Project.Properties[Host] = results[2].ToString();
            Project.Properties[Root] = results[3].ToString();
        }
        #endregion Override implementation of Task

    }
}
