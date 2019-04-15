using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.wpf.PropertyGrid.Extensions.EditorTemplates;

namespace radish.core.Commands
{

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class w3world : RAD_Command
    {
        public w3world()
        {
            Name = "w3world";
            Version = "v0.6.0-pre";
        }

        /// <summary>
        /// Required
        /// </summary>
        [CategoryAttribute("1 Required")]
        [DescriptionAttribute("NAME -- encode (wildcards allowed)")]
        [RADName("--encode")]
        public string Encode { get; set; }

        [CategoryAttribute("1 Required"),
        DescriptionAttribute("DIRECTORY -- define directory of repository. can be used with --encode. default is repo.foliage/.")]
        [Editor(typeof(PropertyGridFolderPicker), typeof(PropertyGridFolderPicker))]
        [RADName("--foliage-dir")]
        public string FoliageDirectory { get; set; }

        [CategoryAttribute("1 Required"),
        DescriptionAttribute("FILE.w2w -- decodes a w2w file into an encodeable yml definition.")]
        [Editor(typeof(PropertyGridFilePicker), typeof(PropertyGridFilePicker))]
        [RADName("--decode")]
        public string Decode { get; set; }

        [CategoryAttribute("1 Required"),
        DescriptionAttribute("DIRECTORY -- define directory of repository. can be used with --encode. default is repo.scenes/.")]
        [Editor(typeof(PropertyGridFolderPicker), typeof(PropertyGridFolderPicker))]
        [RADName("--repo-dir")]
        public string RepoDirectory { get; set; }

        [CategoryAttribute("1 Required"),
        DescriptionAttribute("DIRECTORY -- defines root output-directory for generated file(s).")]
        [Editor(typeof(PropertyGridFolderPicker), typeof(PropertyGridFolderPicker))]
        [RADName("--output-dir")]
        public string OutputDirectory { get; set; }

        [CategoryAttribute("2 Optional"),
        DescriptionAttribute("FLAG -- NO INFO.")]
        [RADName("--no-terrain")]
        public bool SkipWorldGeneration { get; set; }

        [CategoryAttribute("2 Optional"),
        DescriptionAttribute("FLAG -- NO INFO.")]
        [RADName("--no-foliage")]
        public bool SkipFoliageGeneration { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class w3env : RAD_Command
    {
        public w3env()
        {
            Name = "w3env";
            Version = "v0.6.0-pre";
        }

        /// <summary>
        /// Required
        /// </summary>


        [CategoryAttribute("1 Required")]
        [DescriptionAttribute("NAME -- encode (wildcards allowed)")]
        [RADName("--encode")]
        public string Encode { get; set; }

        [CategoryAttribute("1 Required"),
        DescriptionAttribute("FILE.env -- decodes an env file into a yml definition.")]
        [Editor(typeof(PropertyGridFilePicker), typeof(PropertyGridFilePicker))]
        [RADName("--decode")]
        public string Decode { get; set; }

        [CategoryAttribute("1 Required"),
        DescriptionAttribute("DIRECTORY -- defines root output-directory for generated file(s).")]
        [Editor(typeof(PropertyGridFolderPicker), typeof(PropertyGridFolderPicker))]
        [RADName("--output-dir")]
        public string OutputDirectory { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class w2quest : RAD_Command
    {
        public w2quest()
        {
            Name = "w2quest";
            Version = "v0.6.0-pre";
        }

        /// <summary>
        /// Required
        /// </summary>
        [CategoryAttribute("1 Required")]
        [DescriptionAttribute("NAME -- encode (wildcards allowed)")]
        [RADName("--encode")]
        public string Encode { get; set; }

        [CategoryAttribute("1 Required"),
        DescriptionAttribute("FILE.w2xxx -- dumps data from a quest file to a yml-definition.")]
        [Editor(typeof(PropertyGridFilePicker), typeof(PropertyGridFilePicker))]
        [RADName("--dump-data")]
        public string DumpData { get; set; }

        [CategoryAttribute("1 Required"),
        DescriptionAttribute("DIRECTORY -- defines root output-directory for generated file(s).")]
        [Editor(typeof(PropertyGridFolderPicker), typeof(PropertyGridFolderPicker))]
        [RADName("--output-dir")]
        public string OutputDirectory { get; set; }

        [CategoryAttribute("1 Required"),
        DescriptionAttribute("DIRECTORY -- define directory of repository. can be used with --encode. default is repo.quests/.")]
        [Editor(typeof(PropertyGridFolderPicker), typeof(PropertyGridFolderPicker))]
        [RADName("--repo-dir")]
        public string RepoDirectory { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class w2scene : RAD_Command
    {
        public w2scene()
        {
            Name = "w2scene";
            Version = "v0.6.0-pre";
        }

        /// <summary>
        /// Required
        /// </summary>
        [CategoryAttribute("1 Required")]
        [DescriptionAttribute("NAME -- encode definitions from file(s) to a w2scene. definitions can be in one or in multiple of the following files: NAME, NAME.yml, NAME.dialogscript.yml, NAME.storyboard.yml, NAME.production.yml. in addition all *.repo.yml files found in the repository directory will be loaded")]
        [RADName("--encode")]
        public string Encode { get; set; }

        [CategoryAttribute("1 Required"),
        DescriptionAttribute("FLAG -- do not generate input csv for w3strings encoder.")]
        [RADName("--no-strings-csv")]
        public bool No_strings_csv { get; set; }

        [CategoryAttribute("1 Required"),
        DescriptionAttribute("DIRECTORY -- defines output-directory for generated file. can be used with --encode and --dump-assets.")]
        [Editor(typeof(PropertyGridFolderPicker), typeof(PropertyGridFolderPicker))]
        [RADName("--output-dir")]
        public string OutputDirectory { get; set; }

        [CategoryAttribute("1 Required"),
        DescriptionAttribute("DIRECTORY -- define directory of repository. can be used with --encode. default is repo.scenes/.")]
        [Editor(typeof(PropertyGridFolderPicker), typeof(PropertyGridFolderPicker))]
        [RADName("--repo-dir")]
        public string RepoDirectory { get; set; }

        [CategoryAttribute("1 Required"),
        DescriptionAttribute("FILE.w2scene -- dumps actor, camera, animations, mimics, mimics animations from a w2scene file to a repository compatible format. output will be named: FILE.repo.yml")]
        [Editor(typeof(PropertyGridFilePicker), typeof(PropertyGridFilePicker))]
        [RADName("--dump-assets")]
        public string DumpAssets { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class w3speech : RAD_Command
    {
        public w3speech()
        {
            Name = "w3speech";
            Version = "v0.6.0-pre";
        }

        /// <summary>
        /// Required
        /// </summary>
        [CategoryAttribute("1 Required"),
        DescriptionAttribute("DIRECTORY -- packs all voice audio <id>[<duration>]*.wem files and lipsync animation <id>*.cr2w files from DIRECTORY into a w3speech file. a language id is required for proper encoding of embedded ids.")]
        [Editor(typeof(PropertyGridFolderPicker), typeof(PropertyGridFolderPicker))]
        [RADName("--pack-w3speech")]
        public string Pack { get; set; }

        [CategoryAttribute("1 Required"),
        DescriptionAttribute("LANGUAGE -- language id which defines the encoding of embedded ids. default is \"en\".")]
        [Editor(typeof(EnumConverter), typeof(EnumConverter))]
        [RADName("--language")]
        public language Language { get; set; }

        [CategoryAttribute("1 Required"),
        DescriptionAttribute("DIRECTORY -- creates for every <id>*.wem file in DIRECTORY a lipsync animation <id>.cr2w file from an <id>.lipsyncanim.csv file. if no lipsyncanim.csv is found a placeholder lipsync cr2w without any animation is created.")]
        [Editor(typeof(PropertyGridFolderPicker), typeof(PropertyGridFolderPicker))]
        [RADName("--encode-cr2w")]
        public string Encode { get; set; }

        [CategoryAttribute("1 Required"),
        DescriptionAttribute("DIRECTORY -- defines output-directory for generated file. can be used with --encode and --dump-assets.")]
        [Editor(typeof(PropertyGridFolderPicker), typeof(PropertyGridFolderPicker))]
        [RADName("--output-dir")]
        public string OutputDirectory { get; set; }

        [CategoryAttribute("1 Required"),
        DescriptionAttribute("FILE.w3speech -- unpacks all voice audio wem files and lipsync animation cr2w files from FILE.w3speech into a subdirectory.")]
        [Editor(typeof(PropertyGridFilePicker), typeof(PropertyGridFilePicker))]
        [RADName("--unpack-w3speech")]
        public string Unpack { get; set; }

        [CategoryAttribute("1 Required"),
        DescriptionAttribute("FILE.csv -- csv file with <id>s and their associated text lines. format as for the w3 strings encoder. in combination with --unpack-w3speech (shortened parts of) lines are used for additional naming of audio files.")]
        [Editor(typeof(PropertyGridFilePicker), typeof(PropertyGridFilePicker))]
        [RADName("--strings-file")]
        public string StringsFile { get; set; }

        [CategoryAttribute("1 Required"),
        DescriptionAttribute("FILE.cr2w -- dumps pose weights from lipsync animation cr2w files (wildcards allowed).")]
        [Editor(typeof(PropertyGridFilePicker), typeof(PropertyGridFilePicker))]
        [RADName("--dump-poseweights")]
        public string DumpPoseweights { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [CategoryAttribute("2 Optional"),
        DescriptionAttribute("FILE.w3speech -- DEBUG: unpack and pack w3speech file")]
        [Editor(typeof(PropertyGridFilePicker), typeof(PropertyGridFilePicker))]
        [RADName("--test-packer")]
        public string TestEncoder { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class w3speech_phoneme_extractor : RAD_Command
    {
        public w3speech_phoneme_extractor()
        {
            Name = "w3speech-phoneme-extractor";
        }

        /// <summary>
        /// Required
        /// </summary>
        [CategoryAttribute("1 Required"),
        DescriptionAttribute("FILE.wav -- extracts timed phoneme information from <id>*.wav (uncompressed PCM 16kHz mono) file(s) (wildcards allowed) and saves it as <id>.phonemes file(s). requires a strings-file with matching text lines for every <id> (see --strings-file)")]
        [Editor(typeof(PropertyGridFilePicker), typeof(PropertyGridFilePicker))]
        [RADName("--extract")]
        public string Extract { get; set; }

        [CategoryAttribute("1 Required"),
         DescriptionAttribute("FILE.csv -- csv file with <id>s and their associated text lines. format as for the w3 strings encoder.")]
        [Editor(typeof(PropertyGridFilePicker), typeof(PropertyGridFilePicker))]
        [RADName("--strings-file")]
        public string StringsFile { get; set; }

        [CategoryAttribute("1 Required"),
        DescriptionAttribute("DIRECTORY -- defines data-directory containing pocketsphinx and eSpeak data. default is ./data/.")]
        [Editor(typeof(PropertyGridFolderPicker), typeof(PropertyGridFolderPicker))]
        [RADName("--data-dir")]
        public string DataDirectory { get; set; }

        [CategoryAttribute("1 Required"),
        DescriptionAttribute("FLAG -- generate phonemes file for every id found in strings-file based solely on the text.")]
        [RADName("--generate-from-text-only")]
        public bool GenerateFromTextOnly { get; set; }

        [CategoryAttribute("1 Required"),
        DescriptionAttribute("LANGUAGE -- language code which defines the used speech/phoneme recognition models and text to phoneme translation. default is en. NOTE: the code must be supported by eSpeak and CMUSphinx. It will be lowercased and mapped to a data directory data / pocketsphinx /< LANGUAGE >  which must exist and contain the appropriate pocketsphinx models, see readme.txt in data/pocketsphinx directory.")]
        [Editor(typeof(EnumConverter), typeof(EnumConverter))]
        [RADName("--language")]
        public language Language { get; set; }

        [CategoryAttribute("1 Required"),
        DescriptionAttribute("DIRECTORY -- defines output-directory for generated file(s).")]
        [Editor(typeof(PropertyGridFolderPicker), typeof(PropertyGridFolderPicker))]
        [RADName("--output-dir")]
        public string OutputDirectory { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class w3speech_lipsync_creator : RAD_Command
    {
        public w3speech_lipsync_creator()
        {
            Name = "w3speech-lipsync-creator";
            Version = "v0.6.0-pre";
        }

        /// <summary>
        /// Required
        /// </summary>
        [CategoryAttribute("1 Required"),
        DescriptionAttribute("FILE.phonemes -- creates for every <id>*.phonemes file (wildcards allowed) a lipsync animation <id>.lipsyncanim.csv file.")]
        [Editor(typeof(PropertyGridFilePicker), typeof(PropertyGridFilePicker))]
        [RADName("--create-lipsync")]
        public string CreateLipsync { get; set; }

        [CategoryAttribute("1 Required"),
         DescriptionAttribute("FILE.yml -- defines settings file to use. default is lipsync_settings.yml in repository.")]
        [Editor(typeof(PropertyGridFilePicker), typeof(PropertyGridFilePicker))]
        [RADName("--settings")]
        public string SettingsFile { get; set; }

        [CategoryAttribute("1 Required"),
        DescriptionAttribute("DIRECTORY -- defines output-directory for generated file(s).")]
        [Editor(typeof(PropertyGridFolderPicker), typeof(PropertyGridFolderPicker))]
        [RADName("--output-dir")]
        public string OutputDirectory { get; set; }

        [CategoryAttribute("1 Required"),
        DescriptionAttribute("DIRECTORY -- define directory of repository for phoneme mappings. default is repo.lipsync/")]
        [Editor(typeof(PropertyGridFolderPicker), typeof(PropertyGridFolderPicker))]
        [RADName("--repo-dir")]
        public string RepoDirectory { get; set; }

        
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
     public class w3strings : RAD_Command
     {
        public w3strings()
        {
            Name = "w3strings";
            Version = "v0.6.0-pre";
        }

        /// <summary>
        /// Required
        /// </summary>
        [CategoryAttribute("1 Required"),
        DescriptionAttribute("FILE.w2w -- decode w3string file to csv file.")]
        [Editor(typeof(PropertyGridFilePicker), typeof(PropertyGridFilePicker))]
        [RADName("--decode")]
        public string Decode { get; set; }

        [CategoryAttribute("1 Required")]
        [DescriptionAttribute("NAME -- encode csv file to w3string file")]
        [Editor(typeof(PropertyGridFilePicker), typeof(PropertyGridFilePicker))]
        [RADName("--encode")]
        public string Encode { get; set; }

        [CategoryAttribute("1 Required"),
        DescriptionAttribute("nnnn -- check assigned string ids to be in id space [211nnnn000..211nnnn999]. required for --encode.")]
        [RADName("--id-space")]
        public uint IdSpace { get; set; }

        [CategoryAttribute("2 Optional"),
        DescriptionAttribute("FLAG -- I know what I'm doing")]
        [RADName("--force-ignore-id-space-check-i-know-what-i-am-doing")]
        public bool ForceIgnoreIdSpace { get; set; }

        [CategoryAttribute("2 Optional"),
        DescriptionAttribute("FLAG -- missing ids are generated in ascending order starting with minium found id. requires at least one id defined in input csv.")]
        [RADName("--auto-generate-missing-ids")]
        public bool AutoGenerateMissingIds { get; set; }

    }


}
