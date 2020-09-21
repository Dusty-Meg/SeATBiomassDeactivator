import os
import sys

import json
import requests


def character_affiliations(character_ids, count=0):
    headers = {}
    headers['Content-Type'] = "application/json"
    headers['Accept'] = "application/json"
    headers['UserAgent'] = os.environ['USER_AGENT']

    ids = []

    for character in character_ids:
        ids.append(character[0])

    response = requests.post(
        "https://esi.evetech.net/v1/characters/affiliation/",
        headers=headers,
        data=json.dumps(ids)
    )

    if response.status_code == 200:
        try:
            return response.json()
        except json.decoder.JSONDecodeError:
            return character_affiliations(character_ids, count + 1)
    elif response.status_code == 502 and count < 5:
        return character_affiliations(character_ids, count + 1)
    else:
        print("Error checking characters!" + str(ids))
        sys.exit(1)
