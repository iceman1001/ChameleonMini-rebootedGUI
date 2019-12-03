using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChameleonMiniGUI.Log
{
    public enum LogEntryType
    {
        LOG_INFO_GENERIC = 0x10, // Unspecific log entry.
        LOG_INFO_CONFIG_SET = 0x11, // Configuration change.
        LOG_INFO_SETTING_SET = 0x12, // Setting change.
        LOG_INFO_UID_SET = 0x13, // UID change.
        LOG_INFO_RESET_APP = 0x20, // Application reset.

        /* Codec */
        LOG_INFO_CODEC_RX_DATA = 0x40, // Currently active codec received data.
        LOG_INFO_CODEC_TX_DATA = 0x41, // Currently active codec sent data.
        LOG_INFO_CODEC_RX_DATA_W_PARITY = 0x42, // Currently active codec received data.
        LOG_INFO_CODEC_TX_DATA_W_PARITY = 0x43, // Currently active codec sent data.

        LOG_INFO_CODEC_SNI_READER_DATA = 0x44, // Sniffing codec receive data from reader
        LOG_INFO_CODEC_SNI_READER_DATA_W_PARITY = 0x45, // Sniffing codec receive data from reader

        LOG_INFO_CODEC_SNI_CARD_DATA = 0x46, // Sniffing codec receive data from card
        LOG_INFO_CODEC_SNI_CARD_DATA_W_PARITY = 0x47, // Sniffing codec receive data from card

        /* App */
        LOG_INFO_APP_CMD_READ = 0x80, // Application processed read command.
        LOG_INFO_APP_CMD_WRITE = 0x81, // Application processed write command.
        LOG_INFO_APP_CMD_INC = 0x84, // Application processed increment command.
        LOG_INFO_APP_CMD_DEC = 0x85, // Application processed decrement command.
        LOG_INFO_APP_CMD_TRANSFER = 0x86, // Application processed transfer command.
        LOG_INFO_APP_CMD_RESTORE = 0x87, // Application processed restore command.
        LOG_INFO_APP_CMD_AUTH = 0x90, // Application processed authentication command.
        LOG_INFO_APP_CMD_HALT = 0x91, // Application processed halt command.
        LOG_INFO_APP_CMD_UNKNOWN = 0x92, // Application processed an unknown command.
        LOG_INFO_APP_AUTHING = 0xA0, // Application is in `authing` state.
        LOG_INFO_APP_AUTHED = 0xA1, // Application is in `auth` state.
        LOG_ERR_APP_AUTH_FAIL = 0xC0, // Application authentication failed.
        LOG_ERR_APP_CHECKSUM_FAIL = 0xC1, // Application had a checksum fail.
        LOG_ERR_APP_NOT_AUTHED = 0xC2, // Application is not authenticated.

        LOG_INFO_SYSTEM_BOOT = 0xFF, // Chameleon boots

        LOG_EMPTY = 0x00  // Empty Log Entry. This is not followed by a length byte nor the two systick bytes nor any data.
    }
}
