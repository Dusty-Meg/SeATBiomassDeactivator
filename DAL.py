import os
import sys

import mariadb


def db_connect():
    try:
        return mariadb.connect(
            user=os.environ['DB_USER'],
            password=os.environ['DB_PASSWORD'],
            host=os.environ['DB_HOST'],
            port=int(os.environ['DB_PORT']),
            database=os.environ['DB_DATABASE']
            )
    except mariadb.Error as e:
        print(f"Error connecting to MariaDB Platform: {e}")
        sys.exit(1)


def all_characters(db_connection):
    cursor = db_connection.cursor()

    cursor.execute(
        "SELECT users.id "
        "FROM users "
        "LEFT JOIN refresh_tokens ON users.id = refresh_tokens.character_id "
        "WHERE deleted_at IS NOT NULL "
        "AND active IS true"
    )

    return cursor.fetchall()


def deactivate_character(db_connection, character_id):
    cursor = db_connection.cursor()

    cursor.execute(
        "UPDATE users "
        "SET active = false "
        "WHERE id = %s ",
        (character_id, )
    )

    db_connection.commit()
