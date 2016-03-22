﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Examine.LuceneEngine.Faceting;
using Examine.LuceneEngine.Providers;
using Lucene.Net.Documents;
using Lucene.Net.Search;

namespace Examine.LuceneEngine.Indexing.ValueTypes
{
    public class SingleType : IndexValueTypeBase, IIndexRangeValueType<float>
    {
        public SingleType(string fieldName, bool store = true)
            : base(fieldName, store)
        {
        }

        protected override void AddSingleValue(Document doc, object value)
        {
            float parsedVal;
            if (!TryConvert(value, out parsedVal))
                return;

            doc.Add(new NumericField(FieldName, Store ? Field.Store.YES : Field.Store.NO, true).SetFloatValue(parsedVal));
            doc.Add(new NumericField(LuceneIndexer.SortedFieldNamePrefix + FieldName, Field.Store.YES, true).SetFloatValue(parsedVal));
        }

        public override Query GetQuery(string query, Searcher searcher, FacetsLoader facetsLoader, IManagedQueryParameters parameters)
        {
            float parsedVal;
            if (!TryConvert(query, out parsedVal))
                return null;

            return GetQuery(parsedVal, parsedVal);
        }

        public Query GetQuery(float? lower, float? upper, bool lowerInclusive = true, bool upperInclusive = true, IManagedQueryParameters parameters = null)
        {
            return NumericRangeQuery.NewFloatRange(FieldName,
                lower ?? float.MinValue,
                upper ?? float.MaxValue, lowerInclusive, upperInclusive);
        }
    }
}