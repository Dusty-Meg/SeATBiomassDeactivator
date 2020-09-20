from datetime import datetime
import sys

import DAL
import ESI


db_connection = DAL.db_connect()

character_ids = DAL.all_characters(db_connection)

affiliations = ESI.character_affiliations(character_ids)

for character in affiliations:
    if character["corporation_id"] == 1000001:
        DAL.deactivate_character(db_connection, character["character_id"])

print(f"Finished successfully at {str(datetime.utcnow())}")
sys.exit(0)
