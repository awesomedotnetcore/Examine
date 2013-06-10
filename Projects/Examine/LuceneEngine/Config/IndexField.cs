﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Examine.LuceneEngine.Config
{
    ///<summary>
    /// A configuration item representing a field to index
    ///</summary>
    public sealed class IndexField : ConfigurationElement, IIndexField
    {
        [ConfigurationProperty("Name", IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)this["Name"];
            }
            set
            {
                this["Name"] = value;
            }
        }

        [ConfigurationProperty("IndexName", IsRequired = false)]
        public string IndexName
        {
            get
            {
                var indexName = (string)this["IndexName"];
                return string.IsNullOrEmpty(indexName) ? Name : indexName;
            }
            set
            {
                this["IndexName"] = value;
            }
        }

        [ConfigurationProperty("EnableSorting", IsRequired = false)]
        public bool EnableSorting
        {
            get
            {
                return (bool)this["EnableSorting"];
            }
            set
            {
                this["EnableSorting"] = value;
            }
        }

        [ConfigurationProperty("Type", IsRequired = false, DefaultValue = "String")]
        public string Type
        {
            get
            {
                return (string)this["Type"];
            }
            set
            {
                this["Type"] = value;
            }
        }

        public override bool Equals(object compareTo)
        {
            if (compareTo is IndexField)
            {
                return this.Name.Equals(((IndexField)compareTo).Name) && 
                   (IndexName == ((IndexField)compareTo).IndexName);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
        
    }
}