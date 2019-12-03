using System;

namespace ChameleonMiniGUI.Log
{
    class LogEntryUtils
    {

        public static string ParseDownloadedLog(byte[] result)
        {
            var s = "";
            var result_size = result.Length;
            var idx = 0;

            while (idx <= result_size)
            {
                if (idx + 4 > result_size)
                    break;

                var entry_type = result[idx++];
                var data_length = result[idx++];
                var timestampArray = new byte[] { result[idx++], result[idx++] };
                int timestamp = (((int)timestampArray[0]) << 8) | ((int)timestampArray[1]);

                if ((data_length > 0) && (idx + data_length <= result_size))
                {
                    byte[] data = new byte[data_length];
                    Array.Copy(result, idx, data, 0, data_length);
                    idx += data_length;
                    s += "[" + timestamp + "] " + ParseLogEntry(entry_type, data);
                }
            }

            return s;
        }

        private static string ParseLogEntry(byte entry_type, byte[] data)
        {
            switch (entry_type)
            {
                case (byte)LogEntryType.LOG_INFO_CONFIG_SET:
                    var configStr = System.Text.Encoding.ASCII.GetString(data);
                    return "CONFIG SET: " + configStr + Environment.NewLine;
                case (byte)LogEntryType.LOG_INFO_SETTING_SET:
                    var settingNr = data[0] - '0';
                    return "SETTING SET: " + settingNr + Environment.NewLine;
                case (byte)LogEntryType.LOG_INFO_GENERIC:
                case (byte)LogEntryType.LOG_INFO_UID_SET:
                case (byte)LogEntryType.LOG_INFO_RESET_APP:
                case (byte)LogEntryType.LOG_INFO_CODEC_RX_DATA:
                case (byte)LogEntryType.LOG_INFO_CODEC_TX_DATA:
                case (byte)LogEntryType.LOG_INFO_CODEC_RX_DATA_W_PARITY:
                case (byte)LogEntryType.LOG_INFO_CODEC_TX_DATA_W_PARITY:
                case (byte)LogEntryType.LOG_INFO_CODEC_SNI_READER_DATA:
                case (byte)LogEntryType.LOG_INFO_CODEC_SNI_READER_DATA_W_PARITY:
                case (byte)LogEntryType.LOG_INFO_CODEC_SNI_CARD_DATA:
                case (byte)LogEntryType.LOG_INFO_CODEC_SNI_CARD_DATA_W_PARITY:
                case (byte)LogEntryType.LOG_INFO_APP_CMD_READ:
                case (byte)LogEntryType.LOG_INFO_APP_CMD_WRITE:
                case (byte)LogEntryType.LOG_INFO_APP_CMD_INC:
                case (byte)LogEntryType.LOG_INFO_APP_CMD_DEC:
                case (byte)LogEntryType.LOG_INFO_APP_CMD_TRANSFER:
                case (byte)LogEntryType.LOG_INFO_APP_CMD_RESTORE:
                case (byte)LogEntryType.LOG_INFO_APP_CMD_AUTH:
                case (byte)LogEntryType.LOG_INFO_APP_CMD_HALT:
                case (byte)LogEntryType.LOG_INFO_APP_CMD_UNKNOWN:
                case (byte)LogEntryType.LOG_INFO_APP_AUTHING:
                case (byte)LogEntryType.LOG_INFO_APP_AUTHED:
                case (byte)LogEntryType.LOG_INFO_SYSTEM_BOOT:
                    return ((LogEntryType)entry_type).ToString().Replace("LOG_INFO_", "").Replace("_", " ") + ": " + BitConverter.ToString(data).Replace("-", " ") + Environment.NewLine;
                case (byte)LogEntryType.LOG_ERR_APP_AUTH_FAIL:
                case (byte)LogEntryType.LOG_ERR_APP_CHECKSUM_FAIL:
                case (byte)LogEntryType.LOG_ERR_APP_NOT_AUTHED:
                    return ((LogEntryType)entry_type).ToString().Replace("LOG_ERR_", "").Replace("_", " ") + ": " + BitConverter.ToString(data).Replace("-", " ") + Environment.NewLine;
                default:
                    return entry_type.ToString("X2") + BitConverter.ToString(data).Replace(" -", " ") + Environment.NewLine;
            }
        }

    }
}
