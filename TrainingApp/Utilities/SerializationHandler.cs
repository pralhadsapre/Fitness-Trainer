using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO.IsolatedStorage;
using System.IO;
using TrainingApp.Models;

namespace TrainingApp.Utilities
{
    public class SerializationHandler
    {
        private DataContractSerializer _mySerializer;
        private IsolatedStorageFile _isoFile;
        private String _targetFileName;

        public SerializationHandler(String targetFileName)
        {
            _mySerializer = new DataContractSerializer(typeof(PlanCurrent));
            _isoFile = IsolatedStorageFile.GetUserStoreForApplication();
            _targetFileName = targetFileName;
        }

        public void SavePlan(PlanCurrent sourceData)
        {
            try
            {
                using (var targetFile = _isoFile.OpenFile(_targetFileName, System.IO.FileMode.OpenOrCreate))
                {
                    _mySerializer.WriteObject(targetFile, sourceData);
                }
            }
            catch (Exception e)
            {
                _isoFile.DeleteFile(_targetFileName);
            }
        }

        public PlanCurrent LoadPlan()
        {
            try
            {
                if (_isoFile.FileExists(_targetFileName))
                    using (var sourceStream = _isoFile.OpenFile(_targetFileName, FileMode.Open))
                    {
                        return (PlanCurrent)_mySerializer.ReadObject(sourceStream);
                    }
                return null;
            }
            catch (Exception exp) { return null; }
        }
    }
}
