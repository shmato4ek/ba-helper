'use strict';

module.exports = {
  async up(queryInterface, Sequelize) {
    await queryInterface.sequelize.query(
      'CREATE EXTENSION IF NOT EXISTS "uuid-ossp";',
    );

    const transaction = await queryInterface.sequelize.transaction();

    // User
    await queryInterface.createTable(
      'User',
      {
        id: {
          type: Sequelize.UUID,
          defaultValue: Sequelize.literal('uuid_generate_v4()'),
          primaryKey: true,
        },

        email: {
          type: Sequelize.STRING(255),
          unique: true,
          allowNull: false,
        },

        fullName: {
          type: Sequelize.STRING(255),
          allowNull: false,
        },

        createdAt: {
          allowNull: false,
          type: Sequelize.DATE,
        },

        updatedAt: {
          allowNull: false,
          type: Sequelize.DATE,
        },
      },
      {
        timestamps: true,
        transaction,
      },
    );

    await transaction.commit();
  },

  async down(queryInterface) {
    await queryInterface.sequelize.query(`
DROP SCHEMA public CASCADE;
CREATE SCHEMA public;    
    `);
  },
};
